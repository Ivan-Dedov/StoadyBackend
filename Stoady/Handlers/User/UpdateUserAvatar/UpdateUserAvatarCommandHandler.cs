using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.User.UpdateUserAvatar
{
    public sealed class UpdateUserAvatarCommandHandler
        : IRequestHandler<UpdateUserAvatarCommand, Unit>
    {
        public Task<Unit> Handle(
            UpdateUserAvatarCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record UpdateUserAvatarCommand(
            long UserId,
            int AvatarId)
        : IRequest<Unit>;
}
