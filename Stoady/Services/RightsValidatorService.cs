using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;
using Stoady.Services.Interfaces;

namespace Stoady.Services
{
    public class RightsValidatorService : IRightsValidatorService
    {
        private readonly IRoleRepository _roleRepository;

        public RightsValidatorService(
            IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> ValidateRights(
            long teamId,
            long userId,
            CancellationToken ct)
        {
            var role = await _roleRepository.GetUserRoleByTeamId(userId, teamId, ct);
            return role.Id == (int) Role.Admin || role.Id == (int) Role.Creator;
        }
    }
}
