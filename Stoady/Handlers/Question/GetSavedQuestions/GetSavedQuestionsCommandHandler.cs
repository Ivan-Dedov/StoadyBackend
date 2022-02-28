using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.Models.Handlers.Question.GetSavedQuestions;

namespace Stoady.Handlers.Question.GetSavedQuestions
{
    public sealed class GetSavedQuestionsCommandHandler
        : IRequestHandler<GetSavedQuestionsCommand, GetSavedQuestionsResponse>
    {
        public Task<GetSavedQuestionsResponse> Handle(
            GetSavedQuestionsCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record GetSavedQuestionsCommand(
            long UserId)
        : IRequest<GetSavedQuestionsResponse>;
}
