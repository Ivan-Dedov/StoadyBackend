using System;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Models.Handlers.Subject.AddSubject;
using Stoady.Models.Handlers.Subject.EditSubject;
using Stoady.Models.Handlers.Subject.GetSubjectInfo;

namespace Stoady.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{subjectId:long:min(1)}")]
        public async Task<GetSubjectInfoResponse> GetSubjectInfo(
            long subjectId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSubject(
            AddSubjectRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{subjectId:long:min(1)}/edit")]
        public async Task<IActionResult> EditSubject(
            long subjectId,
            [FromBody] EditSubjectRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{subjectId:long:min(1)}/remove")]
        public async Task<IActionResult> RemoveSubject(
            long subjectId)
        {
            throw new NotImplementedException();
        }
    }
}
