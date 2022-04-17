using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        /// <summary>
        /// Получает предмет по его ID
        /// </summary>
        Task<SubjectDao> GetSubjectById(
            long id,
            CancellationToken ct);

        /// <summary>
        /// Получает предметы в команде по ее ID
        /// </summary>
        Task<IEnumerable<SubjectDao>> GetSubjectsByTeamId(
            long teamId,
            CancellationToken ct);

        /// <summary>
        /// Добавляет предмет в команду
        /// </summary>
        Task<int> AddSubject(
            AddSubjectParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Редактирует предмет
        /// </summary>
        Task<int> EditSubject(
            EditSubjectParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Удаляет предмет из команды
        /// </summary>
        Task<int> RemoveSubject(
            long id,
            CancellationToken ct);
    }
}
