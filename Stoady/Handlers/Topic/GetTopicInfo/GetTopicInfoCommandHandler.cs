using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.Topic.GetTopicInfo;

namespace Stoady.Handlers.Topic.GetTopicInfo
{
    public sealed record GetTopicInfoCommand(
            long TopicId)
        : IRequest<GetTopicInfoResponse>;

    public sealed class GetTopicInfoCommandHandler
        : IRequestHandler<GetTopicInfoCommand, GetTopicInfoResponse>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<GetTopicInfoCommandHandler> _logger;

        public GetTopicInfoCommandHandler(
            ILogger<GetTopicInfoCommandHandler> logger,
            ITopicRepository topicRepository,
            IQuestionRepository questionRepository)
        {
            _logger = logger;
            _topicRepository = topicRepository;
            _questionRepository = questionRepository;
        }

        public async Task<GetTopicInfoResponse> Handle(
            GetTopicInfoCommand request,
            CancellationToken ct)
        {
            var topicId = request.TopicId;

            var topicTask = _topicRepository.GetTopicById(topicId, ct);
            var questionsTask = _questionRepository.GetQuestionsByTopicId(topicId, ct);

            await Task.WhenAll(topicTask, questionsTask);

            var topic = topicTask.Result;
            var questions = questionsTask.Result;

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
}
