using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Получает пользователя по его идентификатору
        /// </summary>
        Task<UserDao> GetUserById(
            long userId,
            CancellationToken ct);

        /// <summary>
        /// Получает пользователя с паролем по адресу почты
        /// </summary>
        Task<UserWithPasswordDao> GetUserWithPasswordByEmail(
            string email,
            CancellationToken ct);

        /// <summary>
        /// Получает пользователя по адресу почты
        /// </summary>
        Task<UserDao> GetUserByEmail(
            string email,
            CancellationToken ct);

        /// <summary>
        /// Получает команды пользователя по его ID
        /// </summary>
        Task<IEnumerable<TeamDao>> GetTeamsByUserId(
            long userId,
            CancellationToken ct);

        /// <summary>
        /// Добавляет нового пользователя
        /// </summary>
        Task<int> AddUser(
            AddUserParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Изменяет аватар пользователя
        /// </summary>
        Task<int> ChangeUserAvatarById(
            ChangeUserAvatarParameters parameters,
            CancellationToken ct);
    }
}
