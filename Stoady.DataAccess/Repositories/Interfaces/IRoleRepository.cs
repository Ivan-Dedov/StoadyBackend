using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Получает роль по ее ID
        /// </summary>
        Task<RoleDao> GetRoleById(
            long id,
            CancellationToken ct);

        /// <summary>
        /// Получает роль по ее названию
        /// </summary>
        Task<RoleDao> GetRoleByName(
            string name,
            CancellationToken ct);


        /// <summary>
        /// Получает роль участника в команде
        /// </summary>
        Task<RoleDao> GetUserRoleByTeamId(
            long userId,
            long teamId,
            CancellationToken ct);
    }
}
