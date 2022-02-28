using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Team.AddMember
{
    public sealed class AddMemberCommandHandler
        : IRequestHandler<AddMemberCommand, Unit>
    {
        public Task<Unit> Handle(
            AddMemberCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record AddMemberCommand(
            long TeamId,
            string Email)
        : IRequest<Unit>;
}
