using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Question.RemoveSavedQuestion
{
    public sealed record RemoveSavedQuestionCommand(
            long UserId,
            long QuestionId)
        : IRequest<Unit>;

    public sealed class RemoveSavedQuestionCommandHandler
        : IRequestHandler<RemoveSavedQuestionCommand, Unit>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<RemoveSavedQuestionCommandHandler> _logger;

        public RemoveSavedQuestionCommandHandler(
            ILogger<RemoveSavedQuestionCommandHandler> logger,
            IQuestionRepository questionRepository)
        {
            _logger = logger;
            _questionRepository = questionRepository;
        }

        public async Task<Unit> Handle(
            RemoveSavedQuestionCommand request,
            CancellationToken ct)
        {
            var (userId, questionId) = request;

            await _questionRepository.RemoveQuestionFromSaved(
                new RemoveQuestionFromSavedParameters
                {
                    UserId = userId,
                    QuestionId = questionId
                },
                ct);

            return Unit.Value;
        }
    }
}
