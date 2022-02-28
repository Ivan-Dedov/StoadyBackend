using System;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Models.Handlers.Statistics.GetUserStatistics;

namespace Stoady.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId:long:min(1)}")]
        public async Task<GetUserStatisticsResponse> GetUserStatistics(
            long userId)
        {
            throw new NotImplementedException();
        }
    }
}
