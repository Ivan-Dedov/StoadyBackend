using System;
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

            try
            {
                var result = await _questionRepository.SaveQuestion(
                    new SaveQuestionParameters
                    {
                        UserId = userId,
                        QuestionId = questionId
                    },
                    ct);

                if (result != 1)
                {
                    const string message = "Something went wrong when saving this question. Please, try again.";
                    _logger.LogWarning(message);
                    throw new ApplicationException(message);
                }
            }
            catch (Exception ex)
            {
                var message = $"Something went wrong when saving this question: {ex.Message}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
