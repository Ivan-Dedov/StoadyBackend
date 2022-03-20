using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Question.SaveQuestion
{
    public sealed record SaveQuestionCommand(
            long UserId,
            long QuestionId)
        : IRequest<Unit>;

    public sealed class SaveQuestionCommandHandler
        : IRequestHandler<SaveQuestionCommand, Unit>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ILogger<SaveQuestionCommandHandler> _logger;

        public SaveQuestionCommandHandler(
            ILogger<SaveQuestionCommandHandler> logger,
            IQuestionRepository questionRepository)
        {
            _logger = logger;
            _questionRepository = questionRepository;
        }

        public async Task<Unit> Handle(
            SaveQuestionCommand request,
            CancellationToken ct)
        {
            var (userId, questionId) = request;

            await _questionRepository.SaveQuestion(
                new SaveQuestionParameters
                {
                    UserId = userId,
                    QuestionId = questionId
                },
                ct);

            return Unit.Value;
        }
    }
}
