using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Handlers.Topic.AddTopic;
using Stoady.Handlers.Topic.EditTopic;
using Stoady.Handlers.Topic.GetTopicInfo;
using Stoady.Handlers.Topic.RemoveTopic;
using Stoady.Models.Handlers.Topic.AddTopic;
using Stoady.Models.Handlers.Topic.EditTopic;
using Stoady.Models.Handlers.Topic.GetTopicInfo;

namespace Stoady.Controllers
{
    /// <summary>
    /// Темы
    /// </summary>
    [ApiController]
    [Route("topics")]
    public sealed class TopicController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TopicController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить информацию о теме
        /// </summary>
        /// <param name="topicId">ID темы</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{topicId:long:min(1)}")]
        public async Task<GetTopicInfoResponse> GetTopicInfo(
            long topicId,
            CancellationToken token)
        {
            var command = new GetTopicInfoCommand(topicId);

            var result = await _mediator.Send(command, token);

            return result;
        }

        /// <summary>
        /// Добавить тему
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddTopic(
            [FromBody] AddTopicRequest request,
            CancellationToken token)
        {
            var command = new AddTopicCommand(
                request.SubjectId,
                request.TopicName,
                request.TopicDescription);

            await _mediator.Send(command, token);

            return Ok();
        }

        /// <summary>
        /// Редактировать тему
        /// </summary>
        /// <param name="topicId">ID темы</param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("edit/{topicId:long:min(1)}")]
        public async Task<IActionResult> EditTopic(
            long topicId,
            [FromBody] EditTopicRequest request,
            CancellationToken token)
        {
            var command = new EditTopicCommand(
                topicId,
                request.TopicName,
                request.TopicDescription);

            await _mediator.Send(command, token);

            return Ok();
        }

        /// <summary>
        /// Удалить тему
        /// </summary>
        /// <param name="topicId">ID темы</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("remove/{topicId:long:min(1)}")]
        public async Task<IActionResult> RemoveTopic(
            long topicId,
            CancellationToken token)
        {
            var command = new RemoveTopicCommand(topicId);

            await _mediator.Send(command, token);

            return Ok();
        }
    }
}
