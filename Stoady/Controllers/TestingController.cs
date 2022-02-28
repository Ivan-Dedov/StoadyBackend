using System;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Models.Handlers.Testing.SaveTestResults;

namespace Stoady.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class TestingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestingController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> SaveTestResults(
            [FromBody] SaveTestResultsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
