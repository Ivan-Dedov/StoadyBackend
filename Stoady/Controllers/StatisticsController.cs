using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Handlers.Statistics.GetUserStatistics;
using Stoady.Models.Handlers.Statistics.GetUserStatistics;

namespace Stoady.Controllers
{
    /// <summary>
    /// Статистика изучения предметов
    /// </summary>
    [ApiController]
    [Route("stats")]
    public sealed class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить процент выполнения темы пользователем
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{userId:long:min(1)}")]
        public async Task<GetUserStatisticsResponse> GetUserStatistics(
            long userId,
            CancellationToken ct)
        {
            var command = new GetUserStatisticsCommand(userId);

            var result = await _mediator.Send(command, ct);

            return result;
        }
    }
}
