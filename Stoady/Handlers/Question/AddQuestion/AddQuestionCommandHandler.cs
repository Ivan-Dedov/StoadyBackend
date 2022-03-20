using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Question.AddQuestion
{
    public sealed record AddQuestionCommand(
            long TopicId,
            string QuestionText,
            string AnswerText)
        : IRequest<Unit>;

    public sealed class AddQuestionCommandHandler
        : IRequestHandler<AddQuestionCommand, Unit>
    {
        private readonly ILogger<AddQuestionCommandHandler> _logger;
        private readonly IQuestionRepository _questionRepository;
        private readonly ITopicRepository _topicRepository;

        public AddQuestionCommandHandler(
            ILogger<AddQuestionCommandHandler> logger,
            ITopicRepository topicRepository,
            IQuestionRepository questionRepository)
        {
            _logger = logger;
            _topicRepository = topicRepository;
            _questionRepository = questionRepository;
        }

        public async Task<Unit> Handle(
            AddQuestionCommand request,
            CancellationToken ct)
        {
            var (topicId, questionText, answerText) = request;

            if (_topicRepository.GetTopicById(topicId, ct) is null)
            {
                var message = $"Could not find topic with ID = {topicId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            await _questionRepository.AddQuestion(
                new AddQuestionParameters
                {
                    TopicId = topicId,
                    AnswerText = answerText,
                    QuestionText = questionText
                },
                ct);

            return Unit.Value;
        }
    }
}
