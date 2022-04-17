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
    public sealed class SubjectRepository : ISubjectRepository
    {
        /// <inheritdoc />
        public async Task<SubjectDao> GetSubjectById(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QuerySingleOrDefaultAsync<SubjectDao>(
                new CommandDefinition(
                    @"SELECT
                    s.id as Id,
                    s.title as Title,
                    s.description as Description,
                    s.teamId as TeamId
                    FROM subjects s
                    WHERE id = @id",
                    new { id },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SubjectDao>> GetSubjectsByTeamId(
            long teamId,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.QueryAsync<SubjectDao>(
                new CommandDefinition(
                    @"SELECT
                    s.id as Id,
                    s.title as Title,
                    s.description as Description,
                    s.teamId as TeamId
                    FROM subjects s
                    WHERE teamId = @teamId
                    ORDER BY s.Id",
                    new { teamId },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> AddSubject(
            AddSubjectParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"INSERT INTO subjects
                    (title, description, teamId) VALUES 
                    (@title, @description, @teamId)",
                    new
                    {
                        title = parameters.SubjectName,
                        description = parameters.SubjectDescription,
                        teamId = parameters.TeamId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> EditSubject(
            EditSubjectParameters parameters,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"UPDATE subjects
                    SET title = @title,
                        description = @description  
                    WHERE id = @id",
                    new
                    {
                        title = parameters.SubjectName,
                        description = parameters.SubjectDescription,
                        id = parameters.SubjectId
                    },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }

        /// <inheritdoc />
        public async Task<int> RemoveSubject(
            long id,
            CancellationToken ct)
        {
            await using var dbConnection = new NpgsqlConnection(RepositorySettings.ConnectionString);
            return await dbConnection.ExecuteAsync(
                new CommandDefinition(
                    @"DELETE FROM subjects
                    WHERE id = @id",
                    new { id },
                    commandTimeout: RepositorySettings.TimeoutSeconds,
                    cancellationToken: ct));
        }
    }
}
