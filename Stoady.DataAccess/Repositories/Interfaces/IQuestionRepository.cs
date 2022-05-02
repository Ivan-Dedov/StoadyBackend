using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        /// <summary>
        /// Получает все вопросы по ID темы
        /// </summary>
        Task<IEnumerable<QuestionDao>> GetQuestionsByTopicId(
            long topicId,
            CancellationToken ct);

        /// <summary>
        /// Добавляет вопрос
        /// </summary>
        Task<int> AddQuestion(
            AddQuestionParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Редактирует вопрос
        /// </summary>
        Task<int> EditQuestion(
            EditQuestionParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Удаляет вопрос
        /// </summary>
        Task<int> RemoveQuestion(
            long id,
            CancellationToken ct);

        /// <summary>
        /// Получает сохраненные вопросы пользователя
        /// </summary>
        Task<IEnumerable<QuestionDao>> GetSavedQuestions(
            long userId,
            CancellationToken ct);

        /// <summary>
        /// Сохраняет вопрос пользователю
        /// </summary>
        Task<int> SaveQuestion(
            SaveQuestionParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Удаляет вопрос из сохраненных вопросов пользователя
        /// </summary>
        Task<int> RemoveQuestionFromSaved(
            RemoveQuestionFromSavedParameters parameters,
            CancellationToken ct);
    }
}
