using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDao> GetUser(
            string email,
            string password,
            CancellationToken ct);

        Task<UserDao> GetUserByEmail(
            string email,
            CancellationToken ct);

        Task<IEnumerable<TeamDao>> GetTeamsByUserId(
            long userId,
            CancellationToken ct);

        Task<int> AddUser(
            AddUserParameters parameters,
            CancellationToken ct);

        Task<int> ChangeUserAvatarById(
            ChangeUserAvatarParameters parameters,
            CancellationToken ct);
    }
}
