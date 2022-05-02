using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Handlers.Authentication.Authorization;
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
        /// Авторизовать пользователя по почте и паролю
        /// </summary>
        [HttpPost("authorize")]
        public async Task<ActionResult<AuthorizationResponse>> Authorize(
            [FromBody] AuthorizationRequest request,
            CancellationToken ct)
        {
            var command = new AuthorizationCommand(
                request.Email,
                request.Password);

            try
            {
                var result = await _mediator.Send(command, ct);
                return Ok(result);
            }
            catch (ApplicationException)
            {
                return Unauthorized();
            }
        }
    }
}
