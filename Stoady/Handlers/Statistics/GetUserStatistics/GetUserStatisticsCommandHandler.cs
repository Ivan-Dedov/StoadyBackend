using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.Statistics.GetUserStatistics;

namespace Stoady.Handlers.Statistics.GetUserStatistics
{
    public sealed record GetUserStatisticsCommand(
            long UserId)
        : IRequest<GetUserStatisticsResponse>;

    public sealed class GetUserStatisticsCommandHandler
        : IRequestHandler<GetUserStatisticsCommand, GetUserStatisticsResponse>
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public GetUserStatisticsCommandHandler(
            IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<GetUserStatisticsResponse> Handle(
            GetUserStatisticsCommand request,
            CancellationToken ct)
        {
            var userId = request.UserId;

            var results = (await _statisticsRepository
                    .GetStatisticsByUserId(userId, ct))
                .Select(s => new TopicStatistics
                {
                    TopicId = s.TopicId,
                    Result = s.Result
                })
                .ToList();

            return new GetUserStatisticsResponse
            {
                Results = results
            };
        }
    }
}
