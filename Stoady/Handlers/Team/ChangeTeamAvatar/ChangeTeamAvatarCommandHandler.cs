using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;
using Stoady.Services.Interfaces;

namespace Stoady.Handlers.Team.ChangeTeamAvatar
{
    public sealed record ChangeTeamAvatarCommand(
            long ExecutorId,
            long TeamId,
            string TeamAvatar)
        : IRequest<Unit>;

    public sealed class ChangeTeamAvatarCommandHandler
        : IRequestHandler<ChangeTeamAvatarCommand, Unit>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<ChangeTeamAvatarCommandHandler> _logger;
        private readonly IRightsValidatorService _rightsValidator;

        public ChangeTeamAvatarCommandHandler(
            ILogger<ChangeTeamAvatarCommandHandler> logger,
            IRightsValidatorService rightsValidator, ITeamRepository teamRepository)
        {
            _logger = logger;
            _rightsValidator = rightsValidator;
            _teamRepository = teamRepository;
        }

        public async Task<Unit> Handle(
            ChangeTeamAvatarCommand request,
            CancellationToken ct)
        {
            var (executorId, teamId, teamAvatar) = request;

            if (!await _rightsValidator.ValidateRights(teamId, executorId, ct))
            {
                throw new ApplicationException("Cannot update user: no rights.");
            }

            await _teamRepository.ChangeTeamAvatar(
                new ChangeTeamAvatarParameters
                {
                    TeamId = teamId,
                    TeamAvatar = teamAvatar
                },
                ct);

            return Unit.Value;
        }
    }
}
