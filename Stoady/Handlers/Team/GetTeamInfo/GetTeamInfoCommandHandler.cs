using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.Models.Handlers.Team.GetTeamInfo;

namespace Stoady.Handlers.Team.GetTeamInfo
{
    public sealed class GetTeamInfoCommandHandler
        : IRequestHandler<GetTeamInfoCommand, GetTeamInfoResponse>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<GetTeamInfoCommandHandler> _logger;

        public GetTeamInfoCommandHandler(
            StoadyDataContext context,
            ILogger<GetTeamInfoCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GetTeamInfoResponse> Handle(
            GetTeamInfoCommand request,
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

            var team = teams.First();

            var subjects = _context.Subjects
                .Where(x => x.TeamId == teamId);

            return new GetTeamInfoResponse
            {
                Name = team.Name,
                Picture = team.Avatar,
                Subjects = subjects.Select(x =>
                        new SubjectInTeam
                        {
                            Id = x.Id,
                            Name = x.Title,
                            Image = x.Image,
                            Description = x.Description
                        })
                    .ToList()
            };
        }
    }

    public sealed record GetTeamInfoCommand(
            long TeamId)
        : IRequest<GetTeamInfoResponse>;
}
