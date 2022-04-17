using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Topic.RemoveTopic
{
    public sealed record RemoveTopicCommand(
            long TopicId)
        : IRequest<Unit>;

    public sealed class RemoveTopicCommandHandler
        : IRequestHandler<RemoveTopicCommand, Unit>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ILogger<RemoveTopicCommandHandler> _logger;

        public RemoveTopicCommandHandler(
            ILogger<RemoveTopicCommandHandler> logger,
            ITopicRepository topicRepository)
        {
            _logger = logger;
            _topicRepository = topicRepository;
        }

        public async Task<Unit> Handle(
            RemoveTopicCommand request,
            CancellationToken ct)
        {
            var topicId = request.TopicId;

            var result = await _topicRepository.RemoveTopic(topicId, ct);

            if (result != 1)
            {
                const string message = "Something went wrong when removing this topic. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
