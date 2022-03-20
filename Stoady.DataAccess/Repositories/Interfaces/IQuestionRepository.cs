using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task<QuestionDao> GetQuestionById(
            long id,
            CancellationToken ct);

        Task<IEnumerable<QuestionDao>> GetQuestionsByTopicId(
            long topicId,
            CancellationToken ct);

        Task<int> AddQuestion(
            AddQuestionParameters parameters,
            CancellationToken ct);

        Task<int> EditQuestion(
            EditQuestionParameters parameters,
            CancellationToken ct);

        Task<int> RemoveQuestion(
            long id,
            CancellationToken ct);

        Task<IEnumerable<QuestionDao>> GetSavedQuestions(
            long userId,
            CancellationToken ct);

        Task<int> SaveQuestion(
            SaveQuestionParameters parameters,
            CancellationToken ct);

        Task<int> RemoveQuestionFromSaved(
            RemoveQuestionFromSavedParameters parameters,
            CancellationToken ct);
    }
}
