using System;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Stoady.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{userId:long:min(1)}/avatar/set")]
        public async Task<IActionResult> UpdateUserAvatar(
            long userId,
            [FromQuery] int avatarId)
        {
            throw new NotImplementedException();
        }
    }
}
