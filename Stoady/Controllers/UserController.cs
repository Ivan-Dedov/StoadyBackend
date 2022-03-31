using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Handlers.User.GetUserTeams;
using Stoady.Handlers.User.RegisterUser;
using Stoady.Handlers.User.UpdateUserAvatar;
using Stoady.Models.Handlers.User.GetUserTeams;
using Stoady.Models.Handlers.User.RegisterUser;

namespace Stoady.Controllers
{
    /// <summary>
    /// Пользователи
    /// </summary>
    [ApiController]
    [Route("users")]
    public sealed class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить команды, в которых состоит пользователь
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("teams")]
        public async Task<GetUserTeamsResponse> GetUserTeams(
            [FromQuery] long userId,
            CancellationToken token)
        {
            var command = new GetUserTeamsCommand(userId);

            var result = await _mediator.Send(command, token);

            return result;
        }

        /// <summary>
        /// Зарегистрировать нового пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<RegisterUserResponse> RegisterUser(
            RegisterUserRequest request,
            CancellationToken token)
        {
            var command = new RegisterUserCommand(
                request.Username,
                request.Email,
                request.Password,
                request.AvatarId);

            var result = await _mediator.Send(command, token);

            return result;
        }

        /// <summary>
        /// Обновить аватар пользователя
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="avatarId">ID аватара</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{userId:long:min(1)}/avatar/set")]
        public async Task<IActionResult> UpdateUserAvatar(
            long userId,
            [FromQuery] int avatarId,
            CancellationToken token)
        {
            var command = new UpdateUserAvatarCommand(
                userId,
                avatarId);

            await _mediator.Send(command, token);

            return Ok();
        }
    }
}
