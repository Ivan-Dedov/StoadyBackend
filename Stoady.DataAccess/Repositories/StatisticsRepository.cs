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
    public sealed class StatisticsRepository : IStatisticsRepository
    {
        /// <inheritdoc />
        public async Task<IEnumerable<StatisticsDao>> GetStatisticsByUserId(
            long userId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QueryAsync<StatisticsDao>(
                new CommandDefinition(
                    @"SELECT
                    s.id as Id,
                    s.topicId as TopicId,
                    t.title as TopicName,
                    s.userId as UserId,
                    s.result as Result
                    FROM statistics s
                    INNER JOIN topics t ON t.Id = s.topicId
                    WHERE userId = @userId
                    ORDER BY s.id",
                    new { userId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> AddStatistics(
            AddStatisticsParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"INSERT INTO statistics
                (userId, topicId, result) VALUES 
                (@userId, @topicId, @result)",
                    new
                    {
                        userId = parameters.UserId,
                        topicId = parameters.TopicId,
                        result = parameters.Result
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> EditStatistics(
            EditStatisticsParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"UPDATE statistics
                SET result = @result
                WHERE userId = @userId
                AND topicId = @topicId",
                    new
                    {
                        userId = parameters.UserId,
                        topicId = parameters.TopicId,
                        result = parameters.Result
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }
    }
}
