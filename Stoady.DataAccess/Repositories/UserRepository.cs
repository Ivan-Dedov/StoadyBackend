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
    public sealed class UserRepository : IUserRepository
    {
        private const string ConnectionString = "Server=ec2-52-211-158-144.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9elrdlh8nmq04;Username=rxxaapxbpdyrlk;Password=97ecec32a181a8066f081d64aef963e0dda3c6cda9f3b6af4d19e73d5ca14a01";

        public async Task<UserDao> GetUser(
            string email,
            string password,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<UserDao>(
                @"SELECT 
                    u.id as Id,
                    u.username as Username,
                    u.email as Email,
                    u.refreshToken as RefreshToken,
                    u.refreshTokenExpiryTime as RefreshTokenExpiryTime,
                    u.password as Password,
                    u.avatarId as AvatarId
                    FROM users u
                    WHERE email = @email 
                    AND password = @password",
                new { email, password });
        }

        public async Task<UserDao> GetUserByEmail(
            string email,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<UserDao>(
                @"SELECT 
                    u.id as Id,
                    u.username as Username,
                    u.email as Email,
                    u.refreshToken as RefreshToken,
                    u.refreshTokenExpiryTime as RefreshTokenExpiryTime,
                    u.password as Password,
                    u.avatarId as AvatarId
                    FROM users u
                    WHERE email = @email",
                new { email });
        }

        public async Task<int> AddUser(
            AddUserParameters parameters,
            CancellationToken ct)
        {
            const int defaultAvatarId = 0;

            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"INSERT INTO users
                (username, email, password, avatarId) VALUES 
                (@username, @email, @password, @avatarId)",
                new
                {
                    username = parameters.Username,
                    email = parameters.Email,
                    password = parameters.Password,
                    avatarId = defaultAvatarId
                });
        }

        public async Task<int> ChangeUserAvatarById(
            ChangeUserAvatarParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"UPDATE users
                SET avatarId = @avatarId
                WHERE id = @id",
                new
                {
                    id = parameters.UserId,
                    avatarId = parameters.AvatarId
                });
        }

        public async Task<IEnumerable<TeamDao>> GetTeamsByUserId(
            long userId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QueryAsync<TeamDao>(
                @"SELECT 
                    t.id as Id,
                    t.name as Name,
                    t.avatar as Avatar
                    FROM teamUsers tu
                    LEFT JOIN teams t ON tu.teamId = t.id
                    WHERE tu.userId = @userId",
                new { userId });
        }
    }
}
