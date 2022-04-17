using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;
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
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RemoveMemberCommandHandler> _logger;
        private readonly IRightsValidatorService _rightsValidator;

        public RemoveMemberCommandHandler(
            ITeamRepository teamRepository,
            ILogger<RemoveMemberCommandHandler> logger,
            IRightsValidatorService rightsValidator,
            IRoleRepository roleRepository)
        {
            _teamRepository = teamRepository;
            _logger = logger;
            _rightsValidator = rightsValidator;
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(
            RemoveMemberCommand request,
            CancellationToken ct)
        {
            var (executorId, teamId, userId) = request;

            var userRole = await _roleRepository.GetUserRoleByTeamId(userId, teamId, ct);
            if (userRole is null)
            {
                var message = $"Could not find user with ID = {userId} in team with ID = {teamId}.";
                _logger.LogError(message);
                throw new ApplicationException(message);
            }

            if (userRole.Name == Role.Creator.ToString())
            {
                throw new ApplicationException("You cannot remove the creator from their team.");
            }

            if (await _rightsValidator.ValidateRights(teamId, executorId, ct) is false)
            {
                throw new ApplicationException("You do not have permissions to remove users from this team.");
            }

            var result = await _teamRepository.RemoveMember(userId, teamId, ct);

            if (result != 1)
            {
                const string message = "Something went wrong when removing this user. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
