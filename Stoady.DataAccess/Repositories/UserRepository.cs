using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Stoady.Database.Models.Dto;
using Stoady.Database.Repositories.Parameters;

namespace Stoady.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        public static string BuildConnectionString()
        {
            return $"Host=localhost;" +
                   $"Username=administrator;" +
                   $"Password=assdinsan;" +
                   $"Database=stoady";
        }

        public async Task<UserDto> GetUser(
            GetUserParameters parameters,
            CancellationToken cancellationToken)
        {
            await using var conn = new NpgsqlConnection(BuildConnectionString());
            var user = await conn.QueryFirstAsync<UserDto>(
                @"SELECT * FROM GetUser(@UserEmail, @UserPassword)",
                new
                {
                    UserEmail = parameters.Email,
                    UserPassword = parameters.Password
                });
            return user;
        }
    }
}
