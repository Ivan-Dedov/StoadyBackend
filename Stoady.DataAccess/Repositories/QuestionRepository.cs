using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Npgsql;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.DataAccess.Repositories.Settings;

namespace Stoady.DataAccess.Repositories
{
    public sealed class QuestionRepository : IQuestionRepository
    {
        /// <inheritdoc />
        public async Task<QuestionDao> GetQuestionById(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<QuestionDao>(
                new CommandDefinition(
                    @"SELECT
                    q.id as Id,
                    q.answerText as AnswerText,
                    q.questionText as QuestionText,
                    q.topicId as TopicId
                    FROM questions q
                    WHERE id = @id",
                    new { id },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<QuestionDao>> GetQuestionsByTopicId(
            long topicId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QueryAsync<QuestionDao>(
                new CommandDefinition(
                    @"SELECT
                    q.id as Id,
                    q.answerText as AnswerText,
                    q.questionText as QuestionText,
                    q.topicId as TopicId
                    FROM questions q
                    WHERE topicId = @topicId
                    ORDER BY q.id",
                    new { topicId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> AddQuestion(
            AddQuestionParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"INSERT INTO questions
                    (questionText, answerText, topicId) VALUES 
                    (@questionText, @answerText, @topicId)",
                    new
                    {
                        questionText = parameters.QuestionText,
                        answerText = parameters.AnswerText,
                        topicId = parameters.TopicId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> EditQuestion(
            EditQuestionParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"UPDATE questions
                    SET questionText = @questionText,
                        answerText = @answerText  
                    WHERE id = @id",
                    new
                    {
                        questionText = parameters.QuestionText,
                        answerText = parameters.AnswerText,
                        id = parameters.QuestionId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> RemoveQuestion(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"DELETE FROM questions
                    WHERE id = @id",
                    new { id },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<QuestionDao>> GetSavedQuestions(
            long userId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QueryAsync<QuestionDao>(
                new CommandDefinition(
                    @"SELECT
                    q.id as Id,
                    q.answerText as AnswerText,
                    q.questionText as QuestionText,
                    q.topicId as TopicId
                    FROM userQuestions uq
                    INNER JOIN questions q ON uq.questionId = q.id
                    WHERE userId = @userId
                    ORDER BY q.id",
                    new { userId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> SaveQuestion(
            SaveQuestionParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"INSERT INTO userQuestions
                    (userId, questionId) VALUES 
                    (@userId, @questionId)",
                    new
                    {
                        userId = parameters.UserId,
                        questionId = parameters.QuestionId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> RemoveQuestionFromSaved(
            RemoveQuestionFromSavedParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"DELETE FROM userQuestions uq
                    WHERE userId = @userId
                    AND questionId = @questionId",
                    new
                    {
                        userId = parameters.UserId,
                        questionId = parameters.QuestionId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }
    }
}
