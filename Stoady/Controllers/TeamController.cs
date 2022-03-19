using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Handlers.Team.AddMember;
using Stoady.Handlers.Team.ChangeMemberStatus;
using Stoady.Handlers.Team.ChangeTeamAvatar;
using Stoady.Handlers.Team.CreateTeam;
using Stoady.Handlers.Team.GetTeamInfo;
using Stoady.Handlers.Team.GetTeamMembers;
using Stoady.Handlers.Team.GetUserTeams;
using Stoady.Handlers.Team.RemoveMember;
using Stoady.Handlers.Team.SelectTeam;
using Stoady.Models;
using Stoady.Models.Handlers.Team.GetTeamInfo;
using Stoady.Models.Handlers.Team.GetTeamMembers;
using Stoady.Models.Handlers.Team.GetUserTeams;
using Stoady.Models.Handlers.Team.SelectTeam;

namespace Stoady.Controllers
{
    /// <summary>
    /// Команды
    /// </summary>
    [ApiController]
    [Route("teams")]
    public sealed class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamController(
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
        [HttpGet]
        public async Task<GetUserTeamsResponse> GetUserTeams(
            [FromQuery] long userId,
            CancellationToken token)
        {
            var command = new GetUserTeamsCommand(userId);

            var result = await _mediator.Send(command, token);

            return result;
        }

        [HttpPost("{teamId:long:min(1)}/select")]
        public async Task<SelectTeamResponse> SelectTeam(
            long teamId,
            CancellationToken token)
        {
            var command = new SelectTeamCommand(teamId);

            var result = await _mediator.Send(command, token);

            return result;
        }

        /// <summary>
        /// Получить информацию о команде
        /// </summary>
        /// <param name="teamId">ID команды</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{teamId:long:min(1)}")]
        public async Task<GetTeamInfoResponse> GetTeamInfo(
            long teamId,
            CancellationToken token)
        {
            var command = new GetTeamInfoCommand(teamId);

            var result = await _mediator.Send(command, token);

            return result;
        }

        /// <summary>
        /// Получить участников команды
        /// </summary>
        /// <param name="teamId">ID команды</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{teamId:long:min(1)}/members")]
        public async Task<GetTeamMembersResponse> GetTeamMembers(
            long teamId,
            CancellationToken token)
        {
            var command = new GetTeamMembersCommand(teamId);

            var result = await _mediator.Send(command, token);

            return result;
        }

        /// <summary>
        /// Изменить роль пользователя в команде
        /// </summary>
        /// <param name="executorId">ID пользователя, выполняющего действие</param>
        /// <param name="teamId">ID команды</param>
        /// <param name="userId">ID пользователя</param>
        /// <param name="userRole">Новая роль пользователя</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("{teamId:long:min(1)}/members")]
        public async Task<IActionResult> ChangeMemberStatus(
            [FromHeader] long executorId,
            long teamId,
            [FromQuery] long userId,
            [FromQuery] Role userRole,
            CancellationToken token)
        {
            var command = new ChangeMemberStatusCommand(
                executorId,
                teamId,
                userId,
                userRole);

            await _mediator.Send(command, token);

            return Ok();
        }

        /// <summary>
        /// Создать команду
        /// </summary>
        /// <param name="userId">ID создателя команды</param>
        /// <param name="teamName">Имя команды</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateTeam(
            [FromHeader] long userId,
            [FromQuery] string teamName,
            CancellationToken token)
        {
            var command = new CreateTeamCommand(
                userId,
                teamName);

            await _mediator.Send(command, token);

            return Ok();
        }

        /// <summary>
        /// Изменение аватара команды
        /// </summary>
        /// <param name="executorId">ID пользователя, выполняющего действие</param>
        /// <param name="teamId">ID команды</param>
        /// <param name="avatar">Новая аватарка команды</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{teamId:long:min(1)}/avatar")]
        public async Task<IActionResult> ChangeTeamAvatar(
            [FromHeader] long executorId,
            long teamId,
            [FromQuery] string avatar,
            CancellationToken token)
        {
            var command = new ChangeTeamAvatarCommand(
                executorId,
                teamId,
                avatar);

            await _mediator.Send(command, token);

            return Ok();
        }

        /// <summary>
        /// Добавить пользователя в команду
        /// </summary>
        /// <param name="executorId">ID пользователя, выполняющего действие</param>
        /// <param name="teamId">ID команды</param>
        /// <param name="email">Адрес электронной почты нового участника</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("{teamId:long:min(1)}/members/add")]
        public async Task<IActionResult> AddMember(
            [FromHeader] long executorId,
            long teamId,
            [FromQuery] string email,
            CancellationToken token)
        {
            var command = new AddMemberCommand(
                executorId,
                teamId,
                email);

            await _mediator.Send(command, token);

            return Ok();
        }

        /// <summary>
        /// Удалить пользователя из команды
        /// </summary>
        /// <param name="executorId">ID пользователя, выполняющего действие</param>
        /// <param name="teamId">ID команды</param>
        /// <param name="userId">ID пользователя</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("{teamId:long:min(1)}/members/remove")]
        public async Task<IActionResult> RemoveMember(
            [FromHeader] long executorId,
            long teamId,
            [FromQuery] long userId,
            CancellationToken token)
        {
            var command = new RemoveMemberCommand(
                executorId,
                teamId,
                userId);

            await _mediator.Send(command, token);

            return Ok();
        }
    }
}
