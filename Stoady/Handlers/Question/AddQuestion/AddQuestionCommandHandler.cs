using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.DataAccess.Models.Dao;

namespace Stoady.Handlers.Question.AddQuestion
{
    public sealed class AddQuestionCommandHandler
        : IRequestHandler<AddQuestionCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<AddQuestionCommandHandler> _logger;

        public AddQuestionCommandHandler(
            StoadyDataContext context,
            ILogger<AddQuestionCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            AddQuestionCommand request,
            CancellationToken cancellationToken)
        {
            var (topicId, questionText, answerText) = request;

            if (_context.Topics.Count(x => x.Id == topicId) != 1)
            {
                var message = $"Could not find topic with ID = {topicId}";
                throw new ApplicationException(message);
            }

            await _context.Questions.AddAsync(
                new QuestionDao
                {
                    TopicId = topicId,
                    QuestionText = questionText,
                    AnswerText = answerText
                },
                cancellationToken);

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not add question to topic with ID = {topicId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record AddQuestionCommand(
            long TopicId,
            string QuestionText,
            string AnswerText)
        : IRequest<Unit>;
}
