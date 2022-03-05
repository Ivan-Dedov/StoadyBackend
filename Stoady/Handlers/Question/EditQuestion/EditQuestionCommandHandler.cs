using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;

namespace Stoady.Handlers.Question.EditQuestion
{
    public sealed class EditQuestionCommandHandler
    : IRequestHandler<EditQuestionCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<EditQuestionCommandHandler> _logger;

        public EditQuestionCommandHandler(
            StoadyDataContext context,
            ILogger<EditQuestionCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            EditQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var (questionId, questionText, answerText) = request;

            var questions = _context.Questions
                .Where(x => x.Id == questionId);

            if (questions.Count() != 1)
            {
                var message = $"Could not find question (ID = {questionId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var question = questions.First();
            question.QuestionText = questionText;
            question.AnswerText = answerText;

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not edit question (ID = {questionId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record EditQuestionCommand(
            long QuestionId,
            string QuestionText,
            string AnswerText)
        : IRequest<Unit>;
}
