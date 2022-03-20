using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Topic.EditTopic
{
    public sealed record EditTopicCommand(
            long TopicId,
            string TopicName,
            string TopicDescription)
        : IRequest<Unit>;

    public sealed class EditTopicCommandHandler
        : IRequestHandler<EditTopicCommand, Unit>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ILogger<EditTopicCommandHandler> _logger;

        public EditTopicCommandHandler(
            ILogger<EditTopicCommandHandler> logger,
            ITopicRepository topicRepository)
        {
            _logger = logger;
            _topicRepository = topicRepository;
        }

        public async Task<Unit> Handle(
            EditTopicCommand request,
            CancellationToken ct)
        {
            var (topicId, topicName, topicDescription) = request;

            await _topicRepository.EditTopic(
                new EditTopicParameters
                {
                    TopicId = topicId,
                    TopicName = topicName,
                    TopicDescription = topicDescription
                },
                ct);

            return Unit.Value;
        }
    }
}
