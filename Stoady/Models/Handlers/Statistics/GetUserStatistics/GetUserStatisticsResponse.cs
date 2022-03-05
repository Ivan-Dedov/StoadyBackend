using System.Collections.Generic;

namespace Stoady.Models.Handlers.Statistics.GetUserStatistics
{
    public sealed class GetUserStatisticsResponse
    {
        public List<TopicStatistics> Results { get; init; }
    }
}
