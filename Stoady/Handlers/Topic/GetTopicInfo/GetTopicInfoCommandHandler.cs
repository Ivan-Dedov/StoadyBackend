using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.Models.Handlers.Topic.GetTopicInfo;

namespace Stoady.Handlers.Topic.GetTopicInfo
{
    public sealed class GetTopicInfoCommandHandler
        : IRequestHandler<GetTopicInfoCommand, GetTopicInfoResponse>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<GetTopicInfoCommandHandler> _logger;

        public GetTopicInfoCommandHandler(
            StoadyDataContext context,
            ILogger<GetTopicInfoCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GetTopicInfoResponse> Handle(
            GetTopicInfoCommand request,
            CancellationToken cancellationToken)
        {
            var topicId = request.TopicId;

            var topics = _context.Topics
                .Where(x => x.Id == topicId);

            if (topics.Count() != 1)
            {
                var message = $"Could not find subject (ID = {topicId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var topic = topics.First();

            var questions = _context.Questions
                .Where(x => x.TopicId == topicId);

            return new GetTopicInfoResponse
            {
                Name = topic.Title,
                Description = topic.Description,
                Questions = questions.Select(x =>
                        new QuestionInTopicInfo
                        {
                            Id = x.Id
                        })
                    .ToList()
            };
        }
    }

    public sealed record GetTopicInfoCommand(
            long TopicId)
        : IRequest<GetTopicInfoResponse>;
}
