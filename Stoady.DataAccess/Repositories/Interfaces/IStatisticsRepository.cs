using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;

namespace Stoady.DataAccess.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<IEnumerable<StatisticsDao>> GetStatisticsByUserId(
            long userId,
            CancellationToken ct);

        Task<int> AddStatistics(
            AddStatisticsParameters parameters,
            CancellationToken ct);

        Task<int> EditStatistics(
            EditStatisticsParameters parameters,
            CancellationToken ct);
    }
}
