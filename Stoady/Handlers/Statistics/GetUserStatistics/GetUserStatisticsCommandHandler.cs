using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

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
        private readonly ILogger<GetUserStatisticsCommandHandler> _logger;

        public GetUserStatisticsCommandHandler(
            IStatisticsRepository statisticsRepository,
            ILogger<GetUserStatisticsCommandHandler> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }

        public async Task<GetUserStatisticsResponse> Handle(
            GetUserStatisticsCommand request,
            CancellationToken ct)
        {
            var userId = request.UserId;

            var statistics = await _statisticsRepository
                .GetStatisticsByUserId(userId, ct);

            if (statistics is null)
            {
                var message = $"User with ID = {userId} was not found";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var result = statistics.Select(s => new TopicStatistics
                {
                    TopicId = s.TopicId,
                    TopicName = s.TopicName,
                    Result = s.Result
                })
                .ToList();

            return new GetUserStatisticsResponse
            {
                Results = result
            };
        }
    }
}
