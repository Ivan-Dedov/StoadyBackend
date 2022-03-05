using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.DataAccess.Models.Dao;

namespace Stoady.Handlers.Testing.SaveTestResults
{
    public sealed class SaveTestResultsCommandHandler
        : IRequestHandler<SaveTestResultsCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<SaveTestResultsCommandHandler> _logger;

        public SaveTestResultsCommandHandler(
            StoadyDataContext context,
            ILogger<SaveTestResultsCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            SaveTestResultsCommand request,
            CancellationToken cancellationToken)
        {
            var (userId, topicId, result) = request;

            if (_context.Topics.Count(x => x.Id == topicId) != 1)
            {
                var message = $"Could not find topic with ID = {topicId}";
                throw new ApplicationException(message);
            }

            if (_context.Users.Count(x => x.Id == userId) != 1)
            {
                var message = $"Could not find user with ID = {userId}";
                throw new ApplicationException(message);
            }

            var existingStatistics = _context.Statistics
                .Where(x => x.UserId == userId && x.TopicId == topicId);

            if (existingStatistics.Any())
                EditStatistics(userId, topicId, result);
            else
                await AddNewStatistics(userId, topicId, result);

            if (await _context.SaveChangesAsync() != 1)
            {
                var message =
                    $"Could not save testing results for user with ID = {userId} and topic with ID = {topicId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }

        private void EditStatistics(
            long userId,
            long topicId,
            int result)
        {
            var statistics = _context.Statistics
                .First(x => x.TopicId == topicId && x.UserId == userId);
            statistics.Result = result;
        }

        private ValueTask<EntityEntry<StatisticsDao>> AddNewStatistics(
            long userId,
            long topicId,
            int result)
        {
            return _context.Statistics.AddAsync(
                new StatisticsDao
                {
                    UserId = userId,
                    TopicId = topicId,
                    Result = result
                });
        }
    }

    public sealed record SaveTestResultsCommand(
            long UserId,
            long TopicId,
            int Result)
        : IRequest<Unit>;
}
