using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;

namespace Stoady.Handlers.Question.RemoveSavedQuestion
{
    public sealed class RemoveSavedQuestionCommandHandler
        : IRequestHandler<RemoveSavedQuestionCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<RemoveSavedQuestionCommandHandler> _logger;

        public RemoveSavedQuestionCommandHandler(
            StoadyDataContext context,
            ILogger<RemoveSavedQuestionCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            RemoveSavedQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var (userId, questionId) = request;

            var users = _context.Users
                .Where(x => x.Id == userId);
            var questions = _context.Questions
                .Where(x => x.Id == questionId);


            if (users.Count() != 1 || questions.Count() != 1)
            {
                var message = $"Could not find question with ID = {questionId} for user (ID = {userId}) in saved";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var savedQuestion = _context.UserQuestions
                .First(x => x.UserId == userId && x.QuestionId == questionId);
            _context.UserQuestions.Remove(savedQuestion);

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not remove question with ID = {questionId} for user (ID = {userId}) from saved";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record RemoveSavedQuestionCommand(
            long UserId,
            long QuestionId)
        : IRequest<Unit>;
}
