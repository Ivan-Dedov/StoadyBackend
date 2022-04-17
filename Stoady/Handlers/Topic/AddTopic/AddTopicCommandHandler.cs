using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Topic.AddTopic
{
    public sealed record AddTopicCommand(
            long SubjectId,
            string TopicName,
            string TopicDescription)
        : IRequest<Unit>;

    public sealed class AddTopicCommandHandler
        : IRequestHandler<AddTopicCommand, Unit>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ILogger<AddTopicCommandHandler> _logger;

        public AddTopicCommandHandler(
            ILogger<AddTopicCommandHandler> logger,
            ITopicRepository topicRepository)
        {
            _logger = logger;
            _topicRepository = topicRepository;
        }

        public async Task<Unit> Handle(
            AddTopicCommand request,
            CancellationToken ct)
        {
            var (subjectId, topicName, topicDescription) = request;

            var result = await _topicRepository.AddTopic(
                new AddTopicParameters
                {
                    SubjectId = subjectId,
                    TopicDescription = topicDescription,
                    TopicName = topicName
                },
                ct);

            if (result != 1)
            {
                const string message = "Something went wrong when creating this topic. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }
            return Unit.Value;
        }
    }
}
