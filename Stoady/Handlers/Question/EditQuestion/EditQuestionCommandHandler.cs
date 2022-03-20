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

            await _questionRepository.EditQuestion(
                new EditQuestionParameters
                {
                    QuestionId = questionId,
                    AnswerText = answerText,
                    QuestionText = questionText
                },
                ct);

            return Unit.Value;
        }
    }
}
