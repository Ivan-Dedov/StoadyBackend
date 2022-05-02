using System.Threading;
using System.Threading.Tasks;

using Dapper;

using Npgsql;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.DataAccess.Repositories.Settings;

namespace Stoady.DataAccess.Repositories
{
    public sealed class RoleRepository : IRoleRepository
    {
        /// <inheritdoc />
        public async Task<RoleDao> GetRoleByName(
            string name,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<RoleDao>(
                new CommandDefinition(
                    @"SELECT
                    r.id as Id,
                    r.name as Name
                    FROM roles r
                    WHERE name = @name",
                    new { name },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<RoleDao> GetUserRoleByTeamId(
            long userId,
            long teamId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<RoleDao>(
                new CommandDefinition(
                    @"SELECT
                    r.id as Id,
                    r.name as Name
                    FROM roles r
                    INNER JOIN teamUsers tu ON tu.roleId = r.id
                    WHERE teamId = @teamId
                    AND userId = @userId",
                    new { teamId, userId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }
    }
}
