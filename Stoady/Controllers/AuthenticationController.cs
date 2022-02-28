using System;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Stoady.Controllers
{
    /// <summary>
    /// Аутентификация пользователя.
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
        /// Аутентифицирует пользователя по почте и паролю.
        /// </summary>
        [HttpPost]
        public async Task Authenticate()
        {
            throw new NotImplementedException();
        }
    }
}
