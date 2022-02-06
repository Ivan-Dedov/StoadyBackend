using System.Threading;
using System.Threading.Tasks;
using Stoady.Models;
using Stoady.Repositories.Parameters;

namespace Stoady.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUser(
            GetUserParameters parameters,
            CancellationToken cancellationToken);
    }
}
