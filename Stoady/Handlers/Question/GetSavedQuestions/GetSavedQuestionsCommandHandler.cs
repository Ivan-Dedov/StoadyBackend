using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.Question.GetSavedQuestions;

namespace Stoady.Handlers.Question.GetSavedQuestions
{
    public sealed record GetSavedQuestionsCommand(
            long UserId)
        : IRequest<GetSavedQuestionsResponse>;

    public sealed class GetSavedQuestionsCommandHandler
        : IRequestHandler<GetSavedQuestionsCommand, GetSavedQuestionsResponse>
    {
        private readonly IQuestionRepository _questionRepository;

        public GetSavedQuestionsCommandHandler(
            IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<GetSavedQuestionsResponse> Handle(
            GetSavedQuestionsCommand request,
            CancellationToken ct)
        {
            var userId = request.UserId;

            var savedQuestions = (await _questionRepository
                    .GetSavedQuestions(userId, ct))
                .Select(q =>
                    new SavedQuestion
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        AnswerText = q.AnswerText
                    })
                .ToList();

            return new GetSavedQuestionsResponse
            {
                SavedQuestions = savedQuestions
            };
        }
    }
}
