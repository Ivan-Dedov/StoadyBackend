using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.Models.Handlers.Team.ChangeMemberStatus;

namespace Stoady.Handlers.Team.ChangeMemberStatus
{
    public sealed class ChangeMemberStatusCommandHandler
        : IRequestHandler<ChangeMemberStatusCommand, Unit>
    {
        public Task<Unit> Handle(
            ChangeMemberStatusCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record ChangeMemberStatusCommand(
            long TeamId,
            long UserId,
            Role Role)
        : IRequest<Unit>;
}
