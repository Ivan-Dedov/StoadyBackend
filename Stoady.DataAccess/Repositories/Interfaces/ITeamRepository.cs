using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        /// <summary>
        /// Получает команду по ее ID
        /// </summary>
        Task<TeamDao> GetTeamById(
            long id,
            CancellationToken ct);

        /// <summary>
        /// Получает самую новую команду по названию и создателю
        /// </summary>
        Task<TeamDao> GetTeamByNameAndCreator(
            string name,
            long creatorId,
            CancellationToken ct);

        /// <summary>
        /// Получает участников команды по ее ID
        /// </summary>
        Task<IEnumerable<UserDao>> GetTeamMembersByTeamId(
            long teamId,
            CancellationToken ct);

        /// <summary>
        /// Создает команду
        /// </summary>
        Task<int> CreateTeam(
            CreateTeamParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Добавляет участника в команду
        /// </summary>
        Task<int> AddMember(
            AddMemberParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Изменяет роль участника команды
        /// </summary>
        Task<int> ChangeMemberStatus(
            ChangeMemberStatusParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Редактирует название и картинку команды
        /// </summary>
        Task<int> EditTeam(
            EditTeamParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Удаляет участника из команды
        /// </summary>
        Task<int> RemoveMember(
            long userId,
            long teamId,
            CancellationToken ct);
    }
}
