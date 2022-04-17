using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        /// <summary>
        /// Получает тему по ее ID
        /// </summary>
        Task<TopicDao> GetTopicById(
            long id,
            CancellationToken ct);

        /// <summary>
        /// Получает темы, содержащиеся в предмете по его ID
        /// </summary>
        Task<IEnumerable<TopicDao>> GetTopicsBySubjectId(
            long subjectId,
            CancellationToken ct);

        /// <summary>
        /// Добавляет тему
        /// </summary>
        Task<int> AddTopic(
            AddTopicParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Редактирует тему
        /// </summary>
        Task<int> EditTopic(
            EditTopicParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Удаляет тему
        /// </summary>
        Task<int> RemoveTopic(
            long id,
            CancellationToken ct);
    }
}
