using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

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

        public SaveTestResultsCommandHandler(
            IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<Unit> Handle(
            SaveTestResultsCommand request,
            CancellationToken ct)
        {
            var (userId, topicId, result) = request;

            var existingStatistics = await _statisticsRepository
                .GetStatisticsByUserId(userId, ct);

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

            return Unit.Value;
        }
    }
}
