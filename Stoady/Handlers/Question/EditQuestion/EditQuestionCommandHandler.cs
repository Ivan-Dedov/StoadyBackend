using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Question.EditQuestion
{
    public sealed record EditQuestionCommand(
            long QuestionId,
            string QuestionText,
            string AnswerText)
        : IRequest<Unit>;

    public sealed class EditQuestionCommandHandler
        : IRequestHandler<EditQuestionCommand, Unit>
    {
        private readonly ILogger<EditQuestionCommandHandler> _logger;
        private readonly IQuestionRepository _questionRepository;

        public EditQuestionCommandHandler(
            ILogger<EditQuestionCommandHandler> logger,
            IQuestionRepository questionRepository)
        {
            _logger = logger;
            _questionRepository = questionRepository;
        }

        public async Task<Unit> Handle(
            EditQuestionCommand request,
            CancellationToken ct)
        {
            var (questionId, questionText, answerText) = request;

            var result = await _questionRepository.EditQuestion(
                new EditQuestionParameters
                {
                    QuestionId = questionId,
                    AnswerText = answerText,
                    QuestionText = questionText
                },
                ct);

            if (result != 1)
            {
                const string message = "Something went wrong when editing the question. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
