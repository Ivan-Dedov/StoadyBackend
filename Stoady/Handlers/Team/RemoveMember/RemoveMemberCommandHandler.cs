using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Services.Interfaces;

namespace Stoady.Handlers.Team.RemoveMember
{
    public sealed record RemoveMemberCommand(
            long ExecutorId,
            long TeamId,
            long UserId)
        : IRequest<Unit>;

    public sealed class RemoveMemberCommandHandler
        : IRequestHandler<RemoveMemberCommand, Unit>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<RemoveMemberCommandHandler> _logger;
        private readonly IRightsValidatorService _rightsValidator;

        public RemoveMemberCommandHandler(
            ITeamRepository teamRepository,
            ILogger<RemoveMemberCommandHandler> logger,
            IRightsValidatorService rightsValidator)
        {
            _teamRepository = teamRepository;
            _logger = logger;
            _rightsValidator = rightsValidator;
        }

        public async Task<Unit> Handle(
            RemoveMemberCommand request,
            CancellationToken ct)
        {
            var (executorId, teamId, userId) = request;

            if (!await _rightsValidator.ValidateRights(teamId, executorId, ct))
            {
                throw new ApplicationException("Cannot update user: no rights.");
            }

            await _teamRepository.RemoveMember(userId, teamId, ct);

            return Unit.Value;
        }
    }
}
