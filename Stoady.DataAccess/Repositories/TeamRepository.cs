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
    public sealed class TeamRepository : ITeamRepository
    {
        private const string ConnectionString = "Server=ec2-52-211-158-144.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9elrdlh8nmq04;Username=rxxaapxbpdyrlk;Password=97ecec32a181a8066f081d64aef963e0dda3c6cda9f3b6af4d19e73d5ca14a01";

        public async Task<TeamDao> GetTeamById(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<TeamDao>(
                @"SELECT
                    t.id as Id,
                    t.name as Name,
                    t.avatar as Avatar,
                    t.creatorId as CreatorId
                    FROM teams t
                    WHERE t.id = @id",
                new { id });
        }

        public async Task<TeamDao> GetTeamByNameAndCreator(
            string name,
            long creatorId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QueryFirstOrDefaultAsync<TeamDao>(
                @"SELECT
                    t.id as Id,
                    t.name as Name,
                    t.avatar as Avatar,
                    t.creatorId as CreatorId
                    FROM teams t
                    WHERE t.name = @name
                    AND t.creatorId = @creatorId
                    ORDER BY t.id DESC",
                new { name, creatorId });
        }

        public async Task<IEnumerable<UserDao>> GetTeamMembersByTeamId(
            long teamId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QueryAsync<UserDao>(
                @"SELECT
                    u.id as Id,
                    u.username as Username,
                    u.email as Email,
                    u.refreshToken as RefreshToken,
                    u.refreshTokenExpiryTime as RefreshTokenExpiryTime,
                    u.password as Password,
                    u.avatarId as AvatarId
                    FROM teamUsers tu
                    LEFT JOIN users u ON u.id = tu.userId
                    WHERE tu.teamId = @teamId
                    ORDER BY tu.id",
                new { teamId });
        }

        public async Task<int> CreateTeam(
            CreateTeamParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"INSERT INTO teams
                (name, avatar, creatorId) VALUES 
                (@name, @avatar, @creatorId)",
                new
                {
                    name = parameters.TeamName,
                    avatar = parameters.Avatar,
                    creatorId = parameters.CreatorId
                });
        }

        public async Task<int> AddMember(
            AddMemberParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"INSERT INTO teamUsers
                (teamId, userId, roleId) VALUES 
                (@teamId, @userId, @roleId)",
                new
                {
                    teamId = parameters.TeamId,
                    userId = parameters.UserId,
                    roleId = parameters.RoleId
                });
        }

        public async Task<int> ChangeMemberStatus(
            ChangeMemberStatusParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"UPDATE teamUsers
                SET roleId = @roleId
                WHERE userId = @userId
                AND teamId = @teamId",
                new
                {
                    teamId = parameters.TeamId,
                    userId = parameters.UserId,
                    roleId = parameters.RoleId
                });
        }

        public async Task<int> ChangeTeamAvatar(
            ChangeTeamAvatarParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"UPDATE teams
                SET avatar = @avatar
                WHERE id = @id",
                new
                {
                    id = parameters.TeamId,
                    avatar = parameters.TeamAvatar
                });
        }

        public async Task<int> RemoveMember(
            long userId,
            long teamId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"DELETE FROM teamUsers tu
                WHERE userId = @userId
                AND teamId = @teamId",
                new { userId, teamId });
        }
    }
}
