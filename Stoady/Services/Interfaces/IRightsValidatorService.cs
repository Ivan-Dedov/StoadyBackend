using System.Threading;
using System.Threading.Tasks;

namespace Stoady.Services.Interfaces
{
    public interface IRightsValidatorService
    {
        Task<bool> ValidateRights(
            long teamId,
            long userId,
            CancellationToken ct);
    }
}
