using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.User.RegisterUser
{
    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, Unit>
    {
        public Task<Unit> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record RegisterUserCommand(
            string Username,
            string Email,
            string Password)
        : IRequest<Unit>;
}
