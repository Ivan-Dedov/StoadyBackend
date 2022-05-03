using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

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

            if (await _rightsValidator.ValidateRights(teamId, executorId, ct) is false)
            {
                throw new ApplicationException("You do not have permission to add users to this team.");
            }

            var userTask = _userRepository.GetUserByEmail(email, ct);
            var roleTask = _roleRepository.GetRoleByName(Role.Member.ToString(), ct);

            await Task.WhenAll(userTask, roleTask);

            if (userTask.Result is null)
            {
                throw new ApplicationException($"User with email = {email} does not exist.");
            }

            int result;
            try
            {
                result = await _teamRepository.AddMember(
                    new AddMemberParameters
                    {
                        TeamId = teamId,
                        UserId = userTask.Result.Id,
                        RoleId = roleTask.Result.Id
                    },
                    ct);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    $"Something went wrong when adding user:{Environment.NewLine}{ex.Message}");
            }

            if (result != 1)
            {
                const string message = "Something went wrong when adding this user to the team. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
