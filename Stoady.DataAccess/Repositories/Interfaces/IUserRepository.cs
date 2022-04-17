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
        /// Получает пользователя по его ID
        /// </summary>
        Task<UserDao> GetUserById(
            long id,
            CancellationToken ct);

        /// <summary>
        /// Получает пользователя по почте и захэшированному паролю
        /// </summary>
        Task<UserDao> GetUser(
            string email,
            string password,
            CancellationToken ct);

        /// <summary>
        /// Получает пользователя с паролем по адресу почты
        /// </summary>
        public Task<UserWithPasswordDao> GetUserWithPasswordByEmail(
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
