using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Handlers.Question.AddQuestion;
using Stoady.Handlers.Question.EditQuestion;
using Stoady.Handlers.Question.GetQuestions;
using Stoady.Handlers.Question.GetSavedQuestions;
using Stoady.Handlers.Question.RemoveQuestion;
using Stoady.Handlers.Question.RemoveSavedQuestion;
using Stoady.Handlers.Question.SaveQuestion;
using Stoady.Models.Handlers.Question.AddQuestion;
using Stoady.Models.Handlers.Question.EditQuestion;
using Stoady.Models.Handlers.Question.GetQuestions;
using Stoady.Models.Handlers.Question.GetSavedQuestions;

namespace Stoady.Controllers
{
    /// <summary>
    /// Вопросы
    /// </summary>
    [ApiController]
    [Route("questions")]
    public sealed class QuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Получить вопросы, относящиеся к теме
        /// </summary>
        /// <param name="topicId">ID темы</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("{topicId:long:min(1)}")]
        public async Task<GetQuestionsResponse> GetQuestions(
            long topicId,
            CancellationToken token)
        {
            var command = new GetQuestionsCommand(topicId);

            var result = await _mediator.Send(command, token);

            return result;
        }

        /// <summary>
        /// Получить вопросы пользователя, добавленные в сохраненные
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("saved/{userId:long:min(1)}")]
        public async Task<GetSavedQuestionsResponse> GetSavedQuestions(
            long userId,
            CancellationToken token)
        {
            var command = new GetSavedQuestionsCommand(userId);

            var result = await _mediator.Send(command, token);

            return result;
        }

        /// <summary>
        /// Добавить новый вопрос
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddQuestion(
            AddQuestionRequest request,
            CancellationToken token)
        {
            var command = new AddQuestionCommand(
                request.TopicId,
                request.QuestionText,
                request.AnswerText);

            await _mediator.Send(command, token);

            return Ok();
        }

        /// <summary>
        /// Редактировать вопрос
        /// </summary>
        /// <param name="questionId">ID вопроса</param>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("edit/{questionId:long:min(1)}")]
        public async Task<IActionResult> EditQuestion(
            long questionId,
            [FromBody] EditQuestionRequest request,
            CancellationToken token)
        {
            var command = new EditQuestionCommand(
                questionId,
                request.QuestionText,
                request.AnswerText);

            await _mediator.Send(command, token);

            return Ok();
        }

        /// <summary>
        /// Добавить вопрос в сохраненные
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="questionId">ID вопроса</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut("save/{questionId:long:min(1)}")]
        public async Task<IActionResult> SaveQuestion(
            long questionId,
            [FromQuery] long userId,
            CancellationToken token)
        {
            var command = new SaveQuestionCommand(
                userId,
                questionId);

            await _mediator.Send(command, token);

            return Ok();
        }

        /// <summary>
        /// Удалить вопрос
        /// </summary>
        /// <param name="questionId">ID вопроса</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("remove/{questionId:long:min(1)}")]
        public async Task<IActionResult> RemoveQuestion(
            long questionId,
            CancellationToken token)
        {
            var command = new RemoveQuestionCommand(questionId);

            await _mediator.Send(command, token);

            return Ok();
        }

        /// <summary>
        /// Удалить вопрос из сохраненных
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="questionId">ID вопроса</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("unsave/{questionId:long:min(1)}")]
        public async Task<IActionResult> RemoveSavedQuestion(
            long questionId,
            [FromQuery] long userId,
            CancellationToken token)
        {
            var command = new RemoveSavedQuestionCommand(
                userId,
                questionId);

            await _mediator.Send(command, token);

            return Ok();
        }
    }
}
