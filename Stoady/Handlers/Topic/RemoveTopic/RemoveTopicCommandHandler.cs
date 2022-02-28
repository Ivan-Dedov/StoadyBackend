using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Topic.RemoveTopic
{
    public sealed class RemoveTopicCommandHandler
    : IRequestHandler<RemoveTopicCommand, Unit>
    {
        public Task<Unit> Handle(
            RemoveTopicCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record RemoveTopicCommand(
            long QuestionId)
        : IRequest<Unit>;
}
