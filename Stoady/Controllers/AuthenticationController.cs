using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stoady.Handlers.Authentication;
using Stoady.Models.Handlers.Authentication;

namespace Stoady.Controllers
{
    /// <summary>
    /// User authorization controller.
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Authenticates the user in the application.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The information about the user or an error.</returns>
        [HttpPost]
        public async Task<AuthenticationResponse> Authenticate(
            [FromBody] AuthenticationRequest request,
            CancellationToken cancellationToken)
        {
            var command = new AuthenticationCommand
            {
                Email = request.Email,
                Password = request.Password
            };

            var response = await _mediator.Send(command, cancellationToken);

            return response;
        }
    }
}
