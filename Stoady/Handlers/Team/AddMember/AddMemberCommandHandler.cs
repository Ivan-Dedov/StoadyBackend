using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;
using Stoady.Services.Interfaces;

namespace Stoady.Handlers.Team.AddMember
{
    public sealed record AddMemberCommand(
            long ExecutorId,
            long TeamId,
            string Email)
        : IRequest<Unit>;

    public sealed class AddMemberCommandHandler
        : IRequestHandler<AddMemberCommand, Unit>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IRightsValidatorService _rightsValidator;
        private readonly ILogger<AddMemberCommandHandler> _logger;

        public AddMemberCommandHandler(
            ILogger<AddMemberCommandHandler> logger,
            ITeamRepository teamRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IRightsValidatorService rightsValidator)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _rightsValidator = rightsValidator;
        }

        public async Task<Unit> Handle(
            AddMemberCommand request,
            CancellationToken ct)
        {
            var (executorId, teamId, email) = request;

            if (!await _rightsValidator.ValidateRights(teamId, executorId, ct))
            {
                throw new ApplicationException("Cannot update user: no rights.");
            }

            var user = await _userRepository.GetUserByEmail(email, ct);
            var role = await _roleRepository.GetRoleByName(Role.Member.ToString(), ct);

            await _teamRepository.AddMember(
                new AddMemberParameters
                {
                    TeamId = teamId,
                    UserId = user.Id,
                    RoleId = role.Id
                },
                ct);

            return Unit.Value;
        }
    }
}
