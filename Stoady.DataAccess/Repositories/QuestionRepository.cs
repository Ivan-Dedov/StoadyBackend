using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Npgsql;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.DataAccess.Repositories
{
    public sealed class QuestionRepository : IQuestionRepository
    {
        private const string ConnectionString = "Server=ec2-52-211-158-144.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9elrdlh8nmq04;Username=rxxaapxbpdyrlk;Password=97ecec32a181a8066f081d64aef963e0dda3c6cda9f3b6af4d19e73d5ca14a01";

        public async Task<QuestionDao> GetQuestionById(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<QuestionDao>(
                    @"SELECT
                    q.id as Id,
                    q.answerText as AnswerText,
                    q.questionText as QuestionText,
                    q.topicId as TopicId
                    FROM questions q
                    WHERE id = @id",
                    new { id });
        }

        public async Task<IEnumerable<QuestionDao>> GetQuestionsByTopicId(
            long topicId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QueryAsync<QuestionDao>(
                @"SELECT
                    q.id as Id,
                    q.answerText as AnswerText,
                    q.questionText as QuestionText,
                    q.topicId as TopicId
                    FROM questions q
                    WHERE topicId = @topicId",
                new { topicId });
        }

        public async Task<int> AddQuestion(
            AddQuestionParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"INSERT INTO questions
                (questionText, answerText, topicId) VALUES 
                (@questionText, @answerText, @topicId)",
                new
                {
                    question = parameters.QuestionText,
                    answer = parameters.AnswerText,
                    topicId = parameters.TopicId
                });
        }

        public async Task<int> EditQuestion(
            EditQuestionParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"UPDATE questions
                SET questionText = @questionText,
                    answerText = @answerText  
                WHERE id = @id",
                new
                {
                    question = parameters.QuestionText,
                    answer = parameters.AnswerText,
                    id = parameters.QuestionId
                });
        }

        public async Task<int> RemoveQuestion(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"DELETE FROM questions
                WHERE id = @id",
                new { id });
        }

        public async Task<IEnumerable<QuestionDao>> GetSavedQuestions(
            long userId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QueryAsync<QuestionDao>(
                @"SELECT
                q.id as Id,
                q.answerText as AnswerText,
                q.questionText as QuestionText,
                q.topicId as TopicId
                FROM userQuestions uq
                LEFT JOIN questions q ON uq.questionId = q.id
                WHERE userId = @userId",
                new { userId });
        }

        public async Task<int> SaveQuestion(
            SaveQuestionParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"INSERT INTO userQuestions
                (userId, questionId) VALUES 
                (@userId, @questionId)",
                new
                {
                    userId = parameters.UserId,
                    questionId = parameters.QuestionId
                });
        }

        public async Task<int> RemoveQuestionFromSaved(
            RemoveQuestionFromSavedParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"DELETE FROM userQuestions uq
                WHERE userId = @userId
                AND questionId = @questionId",
                new
                {
                    userId = parameters.UserId,
                    questionId = parameters.QuestionId
                });
        }
    }
}
