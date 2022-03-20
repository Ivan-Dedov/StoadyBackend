using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Npgsql;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.DataAccess.Repositories
{
    public sealed class RoleRepository : IRoleRepository
    {
        private const string ConnectionString = "Server=ec2-52-211-158-144.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9elrdlh8nmq04;Username=rxxaapxbpdyrlk;Password=97ecec32a181a8066f081d64aef963e0dda3c6cda9f3b6af4d19e73d5ca14a01";

        public async Task<RoleDao> GetRoleById(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<RoleDao>(
                @"SELECT
                r.id as Id,
                r.name as Name
                FROM roles r
                WHERE id = @id",
                new { id });
        }

        public async Task<RoleDao> GetRoleByName(
            string name,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<RoleDao>(
                @"SELECT
                r.id as Id,
                r.name as Name
                FROM roles r
                WHERE name = @name",
                new { name });
        }

        public async Task<RoleDao> GetUserRoleByTeamId(
            long userId,
            long teamId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<RoleDao>(
                @"SELECT
                r.id as Id,
                r.name as Name
                FROM roles r
                LEFT JOIN teamUsers tu ON tu.roleId = r.id
                WHERE teamId = @teamId
                AND userId = @userId",
                new { teamId, userId });
        }
    }
}
