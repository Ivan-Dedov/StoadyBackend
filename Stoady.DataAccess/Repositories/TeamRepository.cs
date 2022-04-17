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
    public sealed class TeamRepository : ITeamRepository
    {
        /// <inheritdoc />
        public async Task<TeamDao> GetTeamById(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<TeamDao>(
                new CommandDefinition(
                    @"SELECT
                    t.id as Id,
                    t.name as Name,
                    t.avatar as Avatar,
                    t.creatorId as CreatorId
                    FROM teams t
                    WHERE t.id = @id",
                    new { id },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<TeamDao> GetTeamByNameAndCreator(
            string name,
            long creatorId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QueryFirstOrDefaultAsync<TeamDao>(
                new CommandDefinition(
                    @"SELECT
                    t.id as Id,
                    t.name as Name,
                    t.avatar as Avatar,
                    t.creatorId as CreatorId
                    FROM teams t
                    WHERE t.name = @name
                    AND t.creatorId = @creatorId
                    ORDER BY t.id DESC",
                    new { name, creatorId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<UserDao>> GetTeamMembersByTeamId(
            long teamId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QueryAsync<UserDao>(
                new CommandDefinition(
                    @"SELECT
                    u.id as Id,
                    u.username as Username,
                    u.email as Email,
                    u.refreshToken as RefreshToken,
                    u.refreshTokenExpiryTime as RefreshTokenExpiryTime,
                    u.password as Password,
                    u.avatarId as AvatarId
                    FROM teamUsers tu
                    INNER JOIN users u ON u.id = tu.userId
                    WHERE tu.teamId = @teamId
                    ORDER BY tu.id",
                    new { teamId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> CreateTeam(
            CreateTeamParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"INSERT INTO teams
                    (name, avatar, creatorId) VALUES 
                    (@name, @avatar, @creatorId)",
                    new
                    {
                        name = parameters.TeamName,
                        avatar = parameters.Avatar,
                        creatorId = parameters.CreatorId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> AddMember(
            AddMemberParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"INSERT INTO teamUsers
                    (teamId, userId, roleId) VALUES 
                    (@teamId, @userId, @roleId)",
                    new
                    {
                        teamId = parameters.TeamId,
                        userId = parameters.UserId,
                        roleId = parameters.RoleId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        public async Task<int> ChangeMemberStatus(
            ChangeMemberStatusParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"UPDATE teamUsers
                    SET roleId = @roleId
                    WHERE userId = @userId
                    AND teamId = @teamId",
                    new
                    {
                        teamId = parameters.TeamId,
                        userId = parameters.UserId,
                        roleId = parameters.RoleId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        public async Task<int> EditTeam(
            EditTeamParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"UPDATE teams
                    SET avatar = @avatar,
                        name = @name
                    WHERE id = @id",
                    new
                    {
                        id = parameters.TeamId,
                        name = parameters.TeamName,
                        avatar = parameters.TeamAvatar
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        public async Task<int> RemoveMember(
            long userId,
            long teamId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"DELETE FROM teamUsers tu
                    WHERE userId = @userId
                    AND teamId = @teamId",
                    new { userId, teamId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }
    }
}
