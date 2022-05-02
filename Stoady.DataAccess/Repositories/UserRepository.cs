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
    public sealed class UserRepository : IUserRepository
    {
        /// <inheritdoc />
        public async Task<UserDao> GetUserById(
            long userId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<UserDao>(
                new CommandDefinition(
                    @"SELECT
                    u.id as Id,
                    u.username as Username,
                    u.email as Email,
                    u.avatarId as AvatarId
                    FROM users u
                    WHERE u.id = @id",
                    new { id = userId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<UserDao> GetUserByEmail(
            string email,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<UserDao>(
                new CommandDefinition(
                    @"SELECT
                    u.id as Id,
                    u.username as Username,
                    u.email as Email,
                    u.avatarId as AvatarId
                    FROM users u
                    WHERE email = @email",
                    new { email },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<UserWithPasswordDao> GetUserWithPasswordByEmail(
            string email,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<UserWithPasswordDao>(
                new CommandDefinition(
                    @"SELECT
                    u.id as Id,
                    u.username as Username,
                    u.email as Email,
                    u.avatarId as AvatarId,
                    u.password as Password,
                    u.salt as Salt
                    FROM users u
                    WHERE email = @email",
                    new { email },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> AddUser(
            AddUserParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"INSERT INTO users
                    (username, email, password, avatarId, salt) VALUES 
                    (@username, @email, @password, @avatarId, @salt)",
                    new
                    {
                        username = parameters.Username,
                        email = parameters.Email,
                        password = parameters.Password,
                        avatarId = parameters.AvatarId,
                        salt = parameters.Salt
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> ChangeUserAvatarById(
            ChangeUserAvatarParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"UPDATE users
                    SET avatarId = @avatarId
                    WHERE id = @id",
                    new
                    {
                        id = parameters.UserId,
                        avatarId = parameters.AvatarId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TeamDao>> GetTeamsByUserId(
            long userId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QueryAsync<TeamDao>(
                new CommandDefinition(
                    @"SELECT 
                    t.id as Id,
                    t.name as Name,
                    t.avatar as Avatar
                    FROM teamUsers tu
                    INNER JOIN teams t ON tu.teamId = t.id
                    WHERE tu.userId = @userId",
                    new { userId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }
    }
}
