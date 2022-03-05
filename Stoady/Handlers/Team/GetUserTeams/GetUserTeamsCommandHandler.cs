using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.Models;
using Stoady.Models.Handlers.Team.GetUserTeams;

namespace Stoady.Handlers.Team.GetUserTeams
{
    public sealed class GetUserTeamsCommandHandler
        : IRequestHandler<GetUserTeamsCommand, GetUserTeamsResponse>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<GetUserTeamsCommandHandler> _logger;

        public GetUserTeamsCommandHandler(
            StoadyDataContext context,
            ILogger<GetUserTeamsCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GetUserTeamsResponse> Handle(
            GetUserTeamsCommand request,
            CancellationToken cancellationToken)
        {
            var userId = request.UserId;

            if (_context.Users.Count(x => x.Id == userId) != 1)
            {
                var message = $"Could not find user with ID = {userId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var teams = _context.TeamUser
                .Where(x => x.UserId == userId);

            return new GetUserTeamsResponse
            {
                Teams = teams
                    .Select(x =>
                        new UserTeam
                        {
                            TeamId = x.TeamId,
                            Role = Enum.Parse<Role>(_context.Roles.First(y => y.Id == x.RoleId).Name),
                            TeamName = _context.Teams.First(y => y.Id == x.TeamId).Name,
                            TeamAvatar = _context.Teams.First(y => y.Id == x.TeamId).Avatar
                        })
                    .ToList()
            };
        }
    }

    public sealed record GetUserTeamsCommand(
            long UserId)
        : IRequest<GetUserTeamsResponse>;
}
