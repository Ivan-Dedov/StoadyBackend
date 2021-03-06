using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Handlers.Subject.AddSubject;
using Stoady.Handlers.Subject.EditSubject;
using Stoady.Handlers.Subject.GetSubjectInfo;
using Stoady.Handlers.Subject.RemoveSubject;
using Stoady.Models.Handlers.Subject.AddSubject;
using Stoady.Models.Handlers.Subject.EditSubject;
using Stoady.Models.Handlers.Subject.GetSubjectInfo;

namespace Stoady.Controllers
{
    /// <summary>
    /// Предметы
    /// </summary>
    [ApiController]
    [Route("subjects")]
    public sealed class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить информацию о предмете
        /// </summary>
        /// <param name="subjectId">ID предмета</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{subjectId:long:min(1)}")]
        public async Task<GetSubjectInfoResponse> GetSubjectInfo(
            long subjectId,
            CancellationToken ct)
        {
            var command = new GetSubjectInfoCommand(subjectId);

            var result = await _mediator.Send(command, ct);

            return result;
        }

        /// <summary>
        /// Добавить предмет
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddSubject(
            [FromBody] AddSubjectRequest request,
            CancellationToken ct)
        {
            var command = new AddSubjectCommand(
                request.TeamId,
                request.SubjectName,
                request.SubjectDescription);

            await _mediator.Send(command, ct);

            return Ok();
        }

        /// <summary>
        /// Редактировать предмет
        /// </summary>
        /// <param name="subjectId">ID предмета</param>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("edit/{subjectId:long:min(1)}")]
        public async Task<IActionResult> EditSubject(
            long subjectId,
            [FromBody] EditSubjectRequest request,
            CancellationToken ct)
        {
            var command = new EditSubjectCommand(
                subjectId,
                request.SubjectName,
                request.SubjectDescription);

            await _mediator.Send(command, ct);

            return Ok();
        }

        /// <summary>
        /// Удалить предмет
        /// </summary>
        /// <param name="subjectId">ID предмета</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete("remove/{subjectId:long:min(1)}")]
        public async Task<IActionResult> RemoveSubject(
            long subjectId,
            CancellationToken ct)
        {
            var command = new RemoveSubjectCommand(subjectId);

            await _mediator.Send(command, ct);

            return Ok();
        }
    }
}
