using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.Models;
using Stoady.Models.Handlers.Team.GetTeamMembers;

namespace Stoady.Handlers.Team.GetTeamMembers
{
    public sealed class GetTeamMembersCommandHandler
        : IRequestHandler<GetTeamMembersCommand, GetTeamMembersResponse>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<GetTeamMembersCommandHandler> _logger;

        public GetTeamMembersCommandHandler(
            StoadyDataContext context,
            ILogger<GetTeamMembersCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GetTeamMembersResponse> Handle(
            GetTeamMembersCommand request,
            CancellationToken cancellationToken)
        {
            var teamId = request.TeamId;

            var teams = _context.Teams
                .Where(x => x.Id == teamId);
            if (teams.Count() != 1)
            {
                var message = $"Could not find team with ID = {teamId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var users = _context.TeamUser
                .Where(x => x.TeamId == teamId)
                .Join(_context.Users,
                    x => x.UserId,
                    y => y.Id,
                    (x, y) => new MemberInTeam
                    {
                        Id = y.Id,
                        Username = y.Username,
                        Email = y.Email,
                        Role = Enum.Parse<Role>(_context.Roles.First(z => z.Id == x.RoleId).Name)
                    });

            return new GetTeamMembersResponse
            {
                Members = users.Select(x =>
                        new MemberInTeam
                        {
                            Id = x.Id,
                            Username = x.Username,
                            Email = x.Email,
                            Role = x.Role
                        })
                    .ToList()
            };
        }
    }

    public sealed record GetTeamMembersCommand(
            long TeamId)
        : IRequest<GetTeamMembersResponse>;
}
