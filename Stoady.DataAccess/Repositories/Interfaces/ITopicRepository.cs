using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        Task<TopicDao> GetTopicById(
            long id,
            CancellationToken ct);

        Task<IEnumerable<TopicDao>> GetTopicsBySubjectId(
            long subjectId,
            CancellationToken ct);

        Task<int> AddTopic(
            AddTopicParameters parameters,
            CancellationToken ct);

        Task<int> EditTopic(
            EditTopicParameters parameters,
            CancellationToken ct);

        Task<int> RemoveTopic(
            long id,
            CancellationToken ct);
    }
}
