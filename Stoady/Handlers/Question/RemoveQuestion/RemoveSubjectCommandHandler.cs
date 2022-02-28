using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Question.RemoveQuestion
{
    public sealed class RemoveQuestionCommandHandler
    : IRequestHandler<RemoveQuestionCommand, Unit>
    {
        public Task<Unit> Handle(
            RemoveQuestionCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record RemoveQuestionCommand(
            long QuestionId)
        : IRequest<Unit>;
}
