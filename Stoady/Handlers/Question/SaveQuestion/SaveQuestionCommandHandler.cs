using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Question.SaveQuestion
{
    public sealed class SaveQuestionCommandHandler
    : IRequestHandler<SaveQuestionCommand, Unit>
    {
        public Task<Unit> Handle(
            SaveQuestionCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record SaveQuestionCommand(
            long UserId,
            long QuestionId)
        : IRequest<Unit>;
}
