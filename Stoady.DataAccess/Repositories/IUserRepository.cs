using System.Threading;
using System.Threading.Tasks;
using Stoady.Database.Models.Dto;
using Stoady.Database.Repositories.Parameters;

namespace Stoady.Database.Repositories
{
    public interface IUserRepository
    {
        public Task<UserDto> GetUser(
            GetUserParameters parameters,
            CancellationToken cancellationToken);
    }
}
