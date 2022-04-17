using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Services.Interfaces;

namespace Stoady.Handlers.Team.EditTeam
{
    public sealed record EditTeamCommand(
            long ExecutorId,
            long TeamId,
            string TeamName,
            string TeamAvatar)
        : IRequest<Unit>;

    public sealed class EditTeamCommandHandler
        : IRequestHandler<EditTeamCommand, Unit>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<EditTeamCommandHandler> _logger;
        private readonly IRightsValidatorService _rightsValidator;

        public EditTeamCommandHandler(
            ILogger<EditTeamCommandHandler> logger,
            IRightsValidatorService rightsValidator,
            ITeamRepository teamRepository)
        {
            _logger = logger;
            _rightsValidator = rightsValidator;
            _teamRepository = teamRepository;
        }

        public async Task<Unit> Handle(
            EditTeamCommand request,
            CancellationToken ct)
        {
            var (executorId, teamId, teamName, teamAvatar) = request;

            if (await _rightsValidator.ValidateRights(teamId, executorId, ct) is false)
            {
                throw new ApplicationException("You do not have permission to edit this team.");
            }

            var result = await _teamRepository.EditTeam(
                new EditTeamParameters
                {
                    TeamId = teamId,
                    TeamName = teamName,
                    TeamAvatar = teamAvatar
                },
                ct);

            if (result != 1)
            {
                const string message = "Something went wrong when editing the team. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
