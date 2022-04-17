using System.Threading;
using System.Threading.Tasks;

namespace Stoady.Services.Interfaces
{
    public interface IRightsValidatorService
    {
        /// <summary>
        /// Проверяет, что у пользователя есть права на изменение роли других пользователей
        /// </summary>
        Task<bool> ValidateRights(
            long teamId,
            long userId,
            CancellationToken ct);
    }
}
