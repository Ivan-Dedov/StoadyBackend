using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Handlers.Authentication.Authenticate;
using Stoady.Models.Handlers.Authentication;

namespace Stoady.Controllers
{
    /// <summary>
    /// Аутентификация пользователя
    /// </summary>
    [ApiController]
    [Route("auth")]
    public sealed class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Аутентифицировать пользователя по почте и паролю
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AuthenticationResponse> Authenticate(
            [FromBody] AuthenticationRequest request,
            CancellationToken token)
        {
            var command = new AuthenticationCommand(
                request.Email,
                request.Password);

            var result = await _mediator.Send(command, token);

            return result;
        }
    }
}
