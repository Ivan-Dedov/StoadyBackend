using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Stoady.Models;
using Stoady.Repositories.Parameters;

namespace Stoady.Repositories
{
    public class UserRepository
        : StoadyRepository, IUserRepository
    {
        public async Task<User> GetUser(
            GetUserParameters parameters,
            CancellationToken cancellationToken)
        {
            await using var conn = new NpgsqlConnection(ConnectionString);
            var user = await conn.QueryFirstAsync<User>(
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
