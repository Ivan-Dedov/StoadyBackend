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
    public sealed class SubjectRepository : ISubjectRepository
    {
        private const string ConnectionString = "Server=ec2-52-211-158-144.eu-west-1.compute.amazonaws.com;Port=5432;Database=d9elrdlh8nmq04;Username=rxxaapxbpdyrlk;Password=97ecec32a181a8066f081d64aef963e0dda3c6cda9f3b6af4d19e73d5ca14a01";

        public async Task<SubjectDao> GetSubjectById(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<SubjectDao>(
                @"SELECT
                    s.id as Id,
                    s.title as Title,
                    s.description as Description,
                    s.teamId as TeamId
                    FROM subjects s
                    WHERE id = @id",
                new { id });
        }

        public async Task<IEnumerable<SubjectDao>> GetSubjectsByTeamId(
            long teamId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.QueryAsync<SubjectDao>(
                @"SELECT
                    s.id as Id,
                    s.title as Title,
                    s.description as Description,
                    s.teamId as TeamId
                    FROM subjects s
                    WHERE teamId = @teamId
                    ORDER BY s.Id",
                new { teamId });
        }

        public async Task<int> AddSubject(
            AddSubjectParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"INSERT INTO subjects
                (title, description, teamId) VALUES 
                (@title, @description, @teamId)",
                new
                {
                    title = parameters.SubjectName,
                    description = parameters.SubjectDescription,
                    teamId = parameters.TeamId
                });
        }

        public async Task<int> EditSubject(
            EditSubjectParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"UPDATE subjects
                SET title = @title,
                    description = @description  
                WHERE id = @id",
                new
                {
                    title = parameters.SubjectName,
                    description = parameters.SubjectDescription,
                    id = parameters.SubjectId
                });
        }

        public async Task<int> RemoveSubject(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(ConnectionString);
            return await dbConnection.ExecuteAsync(
                @"DELETE FROM subjects
                WHERE id = @id",
                new { id });
        }
    }
}
