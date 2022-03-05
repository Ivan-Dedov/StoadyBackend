using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.DataAccess.Models.Dao;

namespace Stoady.Handlers.Question.SaveQuestion
{
    public sealed class SaveQuestionCommandHandler
        : IRequestHandler<SaveQuestionCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<SaveQuestionCommandHandler> _logger;

        public SaveQuestionCommandHandler(
            StoadyDataContext context,
            ILogger<SaveQuestionCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            SaveQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var (userId, questionId) = request;

            var users = _context.Users
                .Where(x => x.Id == userId);
            var questions = _context.Questions
                .Where(x => x.Id == questionId);

            var message = $"Could not save question (ID = {questionId}) for user (ID = {userId})";

            if (users.Count() != 1 || questions.Count() != 1)
            {
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            await _context.UserQuestions.AddAsync(
                new UserQuestionDao
                {
                    UserId = userId,
                    QuestionId = questionId
                },
                cancellationToken);

            if (await _context.SaveChangesAsync() != 1)
            {
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }
            return Unit.Value;
        }
    }

    public sealed record SaveQuestionCommand(
            long UserId,
            long QuestionId)
        : IRequest<Unit>;
}
