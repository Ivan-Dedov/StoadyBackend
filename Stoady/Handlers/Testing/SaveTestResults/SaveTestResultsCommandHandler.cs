using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Testing.SaveTestResults
{
    public sealed record SaveTestResultsCommand(
            long UserId,
            long TopicId,
            int Result)
        : IRequest<Unit>;

    public sealed class SaveTestResultsCommandHandler
        : IRequestHandler<SaveTestResultsCommand, Unit>
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly ILogger<SaveTestResultsCommandHandler> _logger;

        public SaveTestResultsCommandHandler(
            IStatisticsRepository statisticsRepository,
            ILogger<SaveTestResultsCommandHandler> logger)
        {
            _statisticsRepository = statisticsRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            SaveTestResultsCommand request,
            CancellationToken ct)
        {
            var (userId, topicId, result) = request;

            var existingStatistics = await _statisticsRepository
                .GetStatisticsByUserId(userId, ct);

            try
            {
                if (existingStatistics.Any(s => s.TopicId == topicId))
                {
                    await _statisticsRepository.EditStatistics(
                        new EditStatisticsParameters
                        {
                            TopicId = topicId,
                            UserId = userId,
                            Result = result
                        },
                        ct);
                }
                else
                {
                    await _statisticsRepository.AddStatistics(
                        new AddStatisticsParameters
                        {
                            TopicId = topicId,
                            UserId = userId,
                            Result = result
                        },
                        ct);
                }
            }
            catch (Exception ex)
            {
                var message =
                    $"Exception occurred when saving statistics for user with ID = {userId} and " +
                    $"topic with ID = {topicId}: {ex.Message}";
                _logger.LogError(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
