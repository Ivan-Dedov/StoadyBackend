using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Handlers.Team.AddMember;
using Stoady.Handlers.Team.ChangeMemberStatus;
using Stoady.Handlers.Team.CreateTeam;
using Stoady.Handlers.Team.EditTeam;
using Stoady.Handlers.Team.GetTeamInfo;
using Stoady.Handlers.Team.GetTeamMembers;
using Stoady.Handlers.Team.RemoveMember;
using Stoady.Handlers.Team.SelectTeam;
using Stoady.Models;
using Stoady.Models.Handlers.Team.EditTeam;
using Stoady.Models.Handlers.Team.GetTeamInfo;
using Stoady.Models.Handlers.Team.GetTeamMembers;
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
        /// Возвращает роль участника в выбранной команде
        /// </summary>
        [HttpPost("{teamId:long:min(1)}/select")]
        public async Task<SelectTeamResponse> SelectTeam(
            long teamId,
            [FromQuery] long userId,
            CancellationToken ct)
        {
            var command = new SelectTeamCommand(userId, teamId);

            var result = await _mediator.Send(command, ct);

            return result;
        }

        /// <summary>
        /// Получить информацию о команде
        /// </summary>
        /// <param name="teamId">ID команды</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{teamId:long:min(1)}")]
        public async Task<GetTeamInfoResponse> GetTeamInfo(
            long teamId,
            CancellationToken ct)
        {
            var command = new GetTeamInfoCommand(teamId);

            var result = await _mediator.Send(command, ct);

            return result;
        }

        /// <summary>
        /// Получить участников команды
        /// </summary>
        /// <param name="teamId">ID команды</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{teamId:long:min(1)}/members")]
        public async Task<GetTeamMembersResponse> GetTeamMembers(
            long teamId,
            CancellationToken ct)
        {
            var command = new GetTeamMembersCommand(teamId);

            var result = await _mediator.Send(command, ct);

            return result;
        }

        /// <summary>
        /// Изменить роль пользователя в команде
        /// </summary>
        /// <param name="executorId">ID пользователя, выполняющего действие</param>
        /// <param name="teamId">ID команды</param>
        /// <param name="userId">ID пользователя</param>
        /// <param name="userRole">Новая роль пользователя</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPut("{teamId:long:min(1)}/members")]
        public async Task<IActionResult> ChangeMemberStatus(
            [FromHeader] long executorId,
            long teamId,
            [FromQuery] long userId,
            [FromQuery] Role userRole,
            CancellationToken ct)
        {
            var command = new ChangeMemberStatusCommand(
                executorId,
                teamId,
                userId,
                userRole);

            await _mediator.Send(command, ct);

            return Ok();
        }

        /// <summary>
        /// Создать команду
        /// </summary>
        /// <param name="userId">ID создателя команды</param>
        /// <param name="teamName">Имя команды</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateTeam(
            [FromQuery] long userId,
            [FromQuery] string teamName,
            CancellationToken ct)
        {
            var command = new CreateTeamCommand(
                userId,
                teamName);

            await _mediator.Send(command, ct);

            return Ok();
        }

        /// <summary>
        /// Редактирование аватара и названия команды
        /// </summary>
        /// <param name="executorId">ID пользователя, выполняющего действие</param>
        /// <param name="teamId">ID команды</param>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("{teamId:long:min(1)}/avatar")]
        public async Task<IActionResult> EditTeam(
            [FromHeader] long executorId,
            long teamId,
            [FromBody] EditTeamRequest request,
            CancellationToken ct)
        {
            var command = new EditTeamCommand(
                executorId,
                teamId,
                request.TeamName,
                request.TeamAvatar);

            await _mediator.Send(command, ct);

            return Ok();
        }

        /// <summary>
        /// Добавить пользователя в команду
        /// </summary>
        /// <param name="executorId">ID пользователя, выполняющего действие</param>
        /// <param name="teamId">ID команды</param>
        /// <param name="email">Адрес электронной почты нового участника</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("{teamId:long:min(1)}/members/add")]
        public async Task<IActionResult> AddMember(
            [FromHeader] long executorId,
            long teamId,
            [FromQuery] string email,
            CancellationToken ct)
        {
            var command = new AddMemberCommand(
                executorId,
                teamId,
                email);

            await _mediator.Send(command, ct);

            return Ok();
        }

        /// <summary>
        /// Удалить пользователя из команды
        /// </summary>
        /// <param name="executorId">ID пользователя, выполняющего действие</param>
        /// <param name="teamId">ID команды</param>
        /// <param name="userId">ID пользователя</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete("{teamId:long:min(1)}/members/remove")]
        public async Task<IActionResult> RemoveMember(
            [FromHeader] long executorId,
            long teamId,
            [FromQuery] long userId,
            CancellationToken ct)
        {
            var command = new RemoveMemberCommand(
                executorId,
                teamId,
                userId);

            await _mediator.Send(command, ct);

            return Ok();
        }
    }
}
