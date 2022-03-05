using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;

namespace Stoady.Handlers.Topic.RemoveTopic
{
    public sealed class RemoveTopicCommandHandler
    : IRequestHandler<RemoveTopicCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<RemoveTopicCommandHandler> _logger;

        public RemoveTopicCommandHandler(
            StoadyDataContext context,
            ILogger<RemoveTopicCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            RemoveTopicCommand request,
            CancellationToken cancellationToken)
        {
            var topicId = request.TopicId;

            var topics = _context.Topics
                .Where(x => x.Id == topicId);

            if (topics.Count() != 1)
            {
                var message = $"Could not find topic (ID = {topicId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var topic = topics.First();
            _context.Topics.Remove(topic);

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not delete subject (ID = {topicId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record RemoveTopicCommand(
            long TopicId)
        : IRequest<Unit>;
}
