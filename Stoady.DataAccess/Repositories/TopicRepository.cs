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
    public sealed class TopicRepository : ITopicRepository
    {
        /// <inheritdoc />
        public async Task<TopicDao> GetTopicById(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<TopicDao>(
                new CommandDefinition(
                    @"SELECT
                    t.id as Id,
                    t.title as Title,
                    t.description as Description,
                    t.subjectId as SubjectId
                    FROM topics t
                    WHERE t.id = @id",
                    new { id },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TopicDao>> GetTopicsBySubjectId(
            long subjectId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QueryAsync<TopicDao>(
                new CommandDefinition(
                    @"SELECT
                    t.id as Id,
                    t.title as Title,
                    t.description as Description,
                    t.subjectId as SubjectId
                    FROM topics t
                    WHERE subjectId = @subjectId
                    ORDER BY t.id",
                    new { subjectId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> AddTopic(
            AddTopicParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"INSERT INTO topics
                    (title, description, subjectId) VALUES 
                    (@title, @description, @subjectId)",
                    new
                    {
                        title = parameters.TopicName,
                        description = parameters.TopicDescription,
                        subjectId = parameters.SubjectId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> EditTopic(
            EditTopicParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"UPDATE topics
                    SET title = @title,
                        description = @description  
                    WHERE id = @id",
                    new
                    {
                        title = parameters.TopicName,
                        description = parameters.TopicDescription,
                        id = parameters.TopicId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> RemoveTopic(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"DELETE FROM topics
                    WHERE id = @id",
                    new { id },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }
    }
}
