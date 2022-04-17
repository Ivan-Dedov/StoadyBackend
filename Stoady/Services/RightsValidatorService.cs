using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;
using Stoady.Services.Interfaces;

namespace Stoady.Services
{
    public class RightsValidatorService : IRightsValidatorService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RightsValidatorService> _logger;

        public RightsValidatorService(
            IRoleRepository roleRepository,
            ILogger<RightsValidatorService> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<bool> ValidateRights(
            long teamId,
            long userId,
            CancellationToken ct)
        {
            var role = await _roleRepository.GetUserRoleByTeamId(userId, teamId, ct);

            if (role is null)
            {
                var message = $"Could not find user with ID = {userId} in team with ID = {teamId}.";
                _logger.LogError(message);
                throw new ApplicationException(message);
            }

            return role.Id is (int) Role.Admin or (int) Role.Creator;
        }
    }
}
