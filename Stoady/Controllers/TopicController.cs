using System;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Models.Handlers.Team.GetTeamInfo;
using Stoady.Models.Handlers.Topic.AddTopic;
using Stoady.Models.Handlers.Topic.EditTopic;

namespace Stoady.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class TopicController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TopicController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{topicId:long:min(1)}")]
        public async Task<GetTeamInfoResponse> GetTopicInfo(
            long topicId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSubject(
            AddTopicRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{topicId:long:min(1)}/edit")]
        public async Task<IActionResult> EditSubject(
            long topicId,
            [FromBody] EditTopicRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{topicId:long:min(1)}/remove")]
        public async Task<IActionResult> RemoveSubject(
            long topicId)
        {
            throw new NotImplementedException();
        }
    }
}
