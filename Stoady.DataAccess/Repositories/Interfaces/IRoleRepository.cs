using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<RoleDao> GetRoleById(
            long id,
            CancellationToken ct);

        Task<RoleDao> GetRoleByName(
            string name,
            CancellationToken ct);

        Task<RoleDao> GetUserRoleByTeamId(
            long userId,
            long teamId,
            CancellationToken ct);
    }
}
