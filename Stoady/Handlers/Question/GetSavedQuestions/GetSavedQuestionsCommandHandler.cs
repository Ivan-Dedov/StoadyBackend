using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.Question.GetSavedQuestions;

namespace Stoady.Handlers.Question.GetSavedQuestions
{
    public sealed record GetSavedQuestionsCommand(
            long UserId)
        : IRequest<GetSavedQuestionsResponse>;

    public sealed class GetSavedQuestionsCommandHandler
        : IRequestHandler<GetSavedQuestionsCommand, GetSavedQuestionsResponse>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<GetSavedQuestionsCommandHandler> _logger;

        public GetSavedQuestionsCommandHandler(
            IQuestionRepository questionRepository,
            ILogger<GetSavedQuestionsCommandHandler> logger)
        {
            _questionRepository = questionRepository;
            _logger = logger;
        }

        public async Task<GetSavedQuestionsResponse> Handle(
            GetSavedQuestionsCommand request,
            CancellationToken ct)
        {
            var userId = request.UserId;

            var savedQuestions = await _questionRepository
                .GetSavedQuestions(userId, ct);

            if (savedQuestions is null)
            {
                var message = $"User with ID = {userId} was not found";
                _logger.LogError(message);
                throw new ApplicationException(message);
            }

            var result = savedQuestions
                .Select(q =>
                    new SavedQuestion
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        AnswerText = q.AnswerText
                    })
                .ToList();

            return new GetSavedQuestionsResponse
            {
                SavedQuestions = result
            };
        }
    }
}
