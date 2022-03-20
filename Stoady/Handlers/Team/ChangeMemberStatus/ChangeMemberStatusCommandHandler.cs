using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;
using Stoady.Services.Interfaces;

namespace Stoady.Handlers.Team.ChangeMemberStatus
{
    public sealed record ChangeMemberStatusCommand(
            long ExecutorId,
            long TeamId,
            long UserId,
            Role Role)
        : IRequest<Unit>;

    public sealed class ChangeMemberStatusCommandHandler
        : IRequestHandler<ChangeMemberStatusCommand, Unit>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRightsValidatorService _rightsValidator;
        private readonly ILogger<ChangeMemberStatusCommandHandler> _logger;

        public ChangeMemberStatusCommandHandler(
            ILogger<ChangeMemberStatusCommandHandler> logger,
            IRightsValidatorService rightsValidator,
            ITeamRepository teamRepository,
            IRoleRepository roleRepository)
        {
            _logger = logger;
            _rightsValidator = rightsValidator;
            _teamRepository = teamRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(
            ChangeMemberStatusCommand request,
            CancellationToken ct)
        {
            var (executorId, teamId, userId, role) = request;

            if (!await _rightsValidator.ValidateRights(teamId, executorId, ct))
            {
                throw new ApplicationException("Cannot update user: no rights.");
            }

            await _teamRepository.ChangeMemberStatus(
                new ChangeMemberStatusParameters
                {
                    TeamId = teamId,
                    UserId = userId,
                    RoleId = (await _roleRepository.GetRoleByName(role.ToString(), ct)).Id
                },
                ct);

            return Unit.Value;
        }
    }
}
