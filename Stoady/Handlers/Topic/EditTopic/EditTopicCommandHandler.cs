using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;

namespace Stoady.Handlers.Topic.EditTopic
{
    public sealed class EditTopicCommandHandler
    : IRequestHandler<EditTopicCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<EditTopicCommandHandler> _logger;

        public EditTopicCommandHandler(
            StoadyDataContext context,
            ILogger<EditTopicCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            EditTopicCommand request,
            CancellationToken cancellationToken)
        {
            var (topicId, topicName, topicDescription) = request;

            var topics = _context.Topics
                .Where(x => x.Id == topicId);

            if (topics.Count() != 1)
            {
                var message = $"Could not find topic (ID = {topicId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var topic = topics.First();
            topic.Title = topicName;
            topic.Description = topicDescription;

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not edit topic (ID = {topicId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record EditTopicCommand(
            long TopicId,
            string TopicName,
            string TopicDescription)
        : IRequest<Unit>;
}
