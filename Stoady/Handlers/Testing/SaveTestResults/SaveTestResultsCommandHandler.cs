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
            ILogger<SaveTestResultsCommandHandler> logger,
            IStatisticsRepository statisticsRepository)
        {
            _logger = logger;
            _statisticsRepository = statisticsRepository;
        }

        public async Task<Unit> Handle(
            SaveTestResultsCommand request,
            CancellationToken ct)
        {
            var (userId, topicId, result) = request;

            var existingStatistics = (await _statisticsRepository
                    .GetStatisticsByUserId(userId, ct))
                .Where(s => s.TopicId == topicId);

            if (existingStatistics.Any())
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

            return Unit.Value;
        }
    }
}
