using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Question.RemoveQuestion
{
    public sealed record RemoveQuestionCommand(
            long QuestionId)
        : IRequest<Unit>;

    public sealed class RemoveQuestionCommandHandler
        : IRequestHandler<RemoveQuestionCommand, Unit>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<RemoveQuestionCommandHandler> _logger;

        public RemoveQuestionCommandHandler(
            ILogger<RemoveQuestionCommandHandler> logger,
            IQuestionRepository questionRepository)
        {
            _logger = logger;
            _questionRepository = questionRepository;
        }

        public async Task<Unit> Handle(
            RemoveQuestionCommand request,
            CancellationToken ct)
        {
            var questionId = request.QuestionId;

            var result = await _questionRepository.RemoveQuestion(questionId, ct);

            if (result != 1)
            {
                const string message = "Something went wrong when removing the question. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
