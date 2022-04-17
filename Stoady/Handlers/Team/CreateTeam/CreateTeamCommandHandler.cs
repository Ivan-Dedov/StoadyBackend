using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;

namespace Stoady.Handlers.Team.CreateTeam
{
    public sealed record CreateTeamCommand(
            long UserId,
            string TeamName)
        : IRequest<Unit>;

    public sealed class CreateTeamCommandHandler
        : IRequestHandler<CreateTeamCommand, Unit>
    {
        private const string DefaultTeamAvatar = "https://ie.wampi.ru/2022/03/20/lake.png";

        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<CreateTeamCommandHandler> _logger;

        public CreateTeamCommandHandler(
            ILogger<CreateTeamCommandHandler> logger,
            ITeamRepository teamRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
        }

        public async Task<Unit> Handle(
            CreateTeamCommand request,
            CancellationToken ct)
        {
            var (userId, teamName) = request;

            var result = await _teamRepository.CreateTeam(
                new CreateTeamParameters
                {
                    TeamName = teamName,
                    Avatar = DefaultTeamAvatar,
                    CreatorId = userId
                },
                ct);

            var addCreatorResult = await _teamRepository.AddMember(
                new AddMemberParameters
                {
                    RoleId = (long) Role.Creator,
                    TeamId = (await _teamRepository.GetTeamByNameAndCreator(teamName, userId, ct)).Id,
                    UserId = userId
                },
                ct);

            if (result != 1 || addCreatorResult != 1)
            {
                const string message = "Something went wrong when creating the team. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
