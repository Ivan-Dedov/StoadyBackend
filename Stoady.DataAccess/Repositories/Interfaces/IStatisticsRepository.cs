using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        /// <summary>
        /// Получает статистику пользователя по его ID
        /// </summary>
        Task<IEnumerable<StatisticsDao>> GetStatisticsByUserId(
            long userId,
            CancellationToken ct);

        /// <summary>
        /// Добавляет новую статистику пользователю
        /// </summary>
        Task<int> AddStatistics(
            AddStatisticsParameters parameters,
            CancellationToken ct);

        /// <summary>
        /// Редактирует существующую статистику пользователя
        /// </summary>
        Task<int> EditStatistics(
            EditStatisticsParameters parameters,
            CancellationToken ct);
    }
}
