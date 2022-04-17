using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.Team.GetTeamInfo;

namespace Stoady.Handlers.Team.GetTeamInfo
{
    public sealed record GetTeamInfoCommand(
            long TeamId)
        : IRequest<GetTeamInfoResponse>;

    public sealed class GetTeamInfoCommandHandler
        : IRequestHandler<GetTeamInfoCommand, GetTeamInfoResponse>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<GetTeamInfoCommandHandler> _logger;

        public GetTeamInfoCommandHandler(
            ILogger<GetTeamInfoCommandHandler> logger,
            ITeamRepository teamRepository,
            ISubjectRepository subjectRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<GetTeamInfoResponse> Handle(
            GetTeamInfoCommand request,
            CancellationToken ct)
        {
            var teamId = request.TeamId;

            var teamTask = _teamRepository.GetTeamById(teamId, ct);
            var subjectsTask = _subjectRepository.GetSubjectsByTeamId(teamId, ct);

            await Task.WhenAll(teamTask, subjectsTask);

            var team = teamTask.Result;
            var subjects = subjectsTask.Result;

            if (team is null || subjects is null)
            {
                var message = $"Could not find team with ID = {teamId}";
                _logger.LogError(message);
                throw new ApplicationException(message);
            }

            return new GetTeamInfoResponse
            {
                Name = team.Name,
                Picture = team.Avatar,
                Subjects = subjects
                    .Select(x =>
                        new SubjectInTeam
                        {
                            Id = x.Id,
                            Name = x.Title,
                            Description = x.Description
                        })
                    .ToList()
            };
        }
    }
}
