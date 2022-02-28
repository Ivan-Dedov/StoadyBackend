using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Team.RemoveMember
{
    public sealed class RemoveMemberCommandHandler
        : IRequestHandler<RemoveMemberCommand, Unit>
    {
        public Task<Unit> Handle(
            RemoveMemberCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record RemoveMemberCommand(
            long TeamId,
            long UserId)
        : IRequest<Unit>;
}
