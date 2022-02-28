using System;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Models.Handlers.Question.AddQuestion;
using Stoady.Models.Handlers.Question.EditQuestion;
using Stoady.Models.Handlers.Question.GetQuestions;
using Stoady.Models.Handlers.Question.GetSavedQuestions;

namespace Stoady.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class QuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{topicId:long:min(1)}")]
        public async Task<GetQuestionsResponse> GetQuestions(
            long topicId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("save/{questionId:long:min(1)}")]
        public async Task<IActionResult> SaveQuestion(
            [FromQuery] long userId,
            long questionId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("saved/{userId:long:min(1)}")]
        public async Task<GetSavedQuestionsResponse> GetSavedQuestions(
            long userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSubject(
            AddQuestionRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{questionId:long:min(1)}/edit")]
        public async Task<IActionResult> EditSubject(
            long questionId,
            [FromBody] EditQuestionRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{questionId:long:min(1)}/remove")]
        public async Task<IActionResult> RemoveSubject(
            long questionId)
        {
            throw new NotImplementedException();
        }
    }
}
