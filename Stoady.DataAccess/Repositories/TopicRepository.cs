using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Npgsql;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.DataAccess.Repositories
{
    public sealed class TopicRepository : ITopicRepository
    {
        private const string ConnectionString = "Server=ec2-52-211-158-144.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9elrdlh8nmq04;Username=rxxaapxbpdyrlk;Password=97ecec32a181a8066f081d64aef963e0dda3c6cda9f3b6af4d19e73d5ca14a01";

        public async Task<TopicDao> GetTopicById(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<TopicDao>(
                @"SELECT
                    t.id as Id,
                    t.title as Title,
                    t.description as Description,
                    t.subjectId as SubjectId
                    FROM topics t
                    WHERE t.id = @id",
                new { id });
        }

        public async Task<IEnumerable<TopicDao>> GetTopicsBySubjectId(
            long subjectId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QueryAsync<TopicDao>(
                @"SELECT
                    t.id as Id,
                    t.title as Title,
                    t.description as Description,
                    t.subjectId as SubjectId
                    FROM topics t
                    WHERE subjectId = @subjectId
                    ORDER BY t.id",
                new { subjectId });
        }

        public async Task<int> AddTopic(
            AddTopicParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"INSERT INTO topics
                (title, description, subjectId) VALUES 
                (@title, @description, @subjectId)",
                new
                {
                    title = parameters.TopicName,
                    description = parameters.TopicDescription,
                    subjectId = parameters.SubjectId
                });
        }

        public async Task<int> EditTopic(
            EditTopicParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"UPDATE topics
                SET title = @title,
                    description = @description  
                WHERE id = @id",
                new
                {
                    title = parameters.TopicName,
                    description = parameters.TopicDescription,
                    id = parameters.TopicId
                });
        }

        public async Task<int> RemoveTopic(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"DELETE FROM topics
                WHERE id = @id",
                new { id });
        }
    }
}
