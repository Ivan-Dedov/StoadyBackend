using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Handlers.Testing.SaveTestResults;
using Stoady.Models.Handlers.Testing.SaveTestResults;

namespace Stoady.Controllers
{
    /// <summary>
    /// Тестирование
    /// </summary>
    [ApiController]
    [Route("tests")]
    public sealed class TestingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestingController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Сохранить результаты тестирования
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveTestResults(
            [FromBody] SaveTestResultsRequest request,
            CancellationToken token)
        {
            var command = new SaveTestResultsCommand(
                request.UserId,
                request.TopicId,
                request.Result);

            await _mediator.Send(command, token);

            return Ok();
        }
    }
}
