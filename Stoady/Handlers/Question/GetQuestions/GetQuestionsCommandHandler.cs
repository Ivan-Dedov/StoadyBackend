using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.Models.Handlers.Question.GetQuestions;

namespace Stoady.Handlers.Question.GetQuestions
{
    public sealed class GetQuestionsCommandHandler
    : IRequestHandler<GetQuestionsCommand, GetQuestionsResponse>
    {
        public Task<GetQuestionsResponse> Handle(
            GetQuestionsCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record GetQuestionsCommand(
            long TopicId)
        : IRequest<GetQuestionsResponse>;
}
