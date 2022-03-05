using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.Models.Handlers.Authentication;

namespace Stoady.Handlers.Authentication.Authenticate
{
    public sealed class AuthenticationCommandHandler
        : IRequestHandler<AuthenticationCommand, AuthenticationResponse>
    {
        public async Task<AuthenticationResponse> Handle(
            AuthenticationCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record AuthenticationCommand(
            string Email,
            string Password)
        : IRequest<AuthenticationResponse>;
}
