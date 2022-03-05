using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.DataAccess.DataContexts;
using Stoady.Models.Handlers.Question.GetSavedQuestions;

namespace Stoady.Handlers.Question.GetSavedQuestions
{
    public sealed class GetSavedQuestionsCommandHandler
        : IRequestHandler<GetSavedQuestionsCommand, GetSavedQuestionsResponse>
    {
        private readonly StoadyDataContext _context;

        public GetSavedQuestionsCommandHandler(
            StoadyDataContext context)
        {
            _context = context;
        }

        public async Task<GetSavedQuestionsResponse> Handle(
            GetSavedQuestionsCommand request,
            CancellationToken cancellationToken)
        {
            var userId = request.UserId;

            var savedQuestions = _context.UserQuestions
                .Where(x => x.UserId == userId)
                .Join(
                    _context.Questions,
                    x => x.QuestionId,
                    y => y.Id,
                    (x, y) =>
                        new SavedQuestion
                        {
                            Id = x.QuestionId,
                            QuestionText = y.QuestionText,
                            AnswerText = y.AnswerText,
                            TopicId = y.TopicId
                        })
                .ToList();

            return new GetSavedQuestionsResponse
            {
                UserId = userId,
                SavedQuestions = savedQuestions
            };
        }
    }

    public sealed record GetSavedQuestionsCommand(
            long UserId)
        : IRequest<GetSavedQuestionsResponse>;
}
