using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;

namespace Stoady.Handlers.Question.RemoveQuestion
{
    public sealed class RemoveQuestionCommandHandler
    : IRequestHandler<RemoveQuestionCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<RemoveQuestionCommandHandler> _logger;

        public RemoveQuestionCommandHandler(
            StoadyDataContext context,
            ILogger<RemoveQuestionCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            RemoveQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var questionId = request.QuestionId;

            var questions = _context.Questions
                .Where(x => x.Id == questionId);

            if (questions.Count() != 1)
            {
                var message = $"Could not find question (ID = {questionId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var question = questions.First();
            _context.Questions.Remove(question);

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not delete question (ID = {questionId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record RemoveQuestionCommand(
            long QuestionId)
        : IRequest<Unit>;
}
