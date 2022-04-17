using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.Question.GetQuestions;

namespace Stoady.Handlers.Question.GetQuestions
{
    public sealed record GetQuestionsCommand(
            long TopicId)
        : IRequest<GetQuestionsResponse>;

    public sealed class GetQuestionsCommandHandler
        : IRequestHandler<GetQuestionsCommand, GetQuestionsResponse>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<GetQuestionsCommandHandler> _logger;

        public GetQuestionsCommandHandler(
            IQuestionRepository questionRepository,
            ILogger<GetQuestionsCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<GetQuestionsResponse> Handle(
            GetQuestionsCommand request,
            CancellationToken ct)
        {
            var topicId = request.TopicId;

            var questions = await _questionRepository
                .GetQuestionsByTopicId(topicId, ct);

            if (questions is null)
            {
                var message = $"Topic with ID = {topicId} was not found";
                _logger.LogError(message);
                throw new ApplicationException(message);
            }

            var result = questions
                .Select(q =>
                    new QuestionInTopic
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        AnswerText = q.AnswerText
                    })
                .ToList();

            return new GetQuestionsResponse
            {
                Questions = result
            };
        }
    }
}
