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
    public sealed class StatisticsRepository : IStatisticsRepository
    {
        private const string ConnectionString = "Server=ec2-52-211-158-144.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9elrdlh8nmq04;Username=rxxaapxbpdyrlk;Password=97ecec32a181a8066f081d64aef963e0dda3c6cda9f3b6af4d19e73d5ca14a01";

        public async Task<IEnumerable<StatisticsDao>> GetStatisticsByUserId(
            long userId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QueryAsync<StatisticsDao>(
                @"SELECT
                    s.id as Id,
                    s.topicId as TopicId,
                    s.userId as UserId,
                    s.result as Result
                    FROM statistics s
                    WHERE userId = @userId",
                new { userId });
        }

        public async Task<int> AddStatistics(
            AddStatisticsParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"INSERT INTO statistics
                (userId, topicId, result) VALUES 
                (@userId, @topicId, @result)",
                new
                {
                    userId = parameters.UserId,
                    topicId = parameters.TopicId,
                    result = parameters.Result
                });
        }

        public async Task<int> EditStatistics(
            EditStatisticsParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"UPDATE statistics
                SET result = @result
                WHERE userId = @userId
                AND topicId = @topicId",
                new
                {
                    userId = parameters.UserId,
                    topicId = parameters.TopicId,
                    result = parameters.Result
                });
        }
    }
}
