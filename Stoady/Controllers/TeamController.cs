using System;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Stoady.Models.Handlers.Team.ChangeMemberStatus;
using Stoady.Models.Handlers.Team.GetTeamInfo;
using Stoady.Models.Handlers.Team.GetTeamMembers;
using Stoady.Models.Handlers.Team.GetUserTeams;

namespace Stoady.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GetUserTeamsResponse> GetUserTeams(
            [FromQuery] long userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{teamId:long:min(1)}")]
        public async Task<GetTeamInfoResponse> GetTeamInfo(
            long teamId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTeam(
            [FromQuery] long userId,
            [FromQuery] string teamName)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{teamId:long:min(1)}/members")]
        public async Task<GetTeamMembersResponse> GetTeamMembers(
            long teamId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{teamId:long:min(1)}/members")]
        public async Task<IActionResult> ChangeMemberStatus(
            long teamId,
            [FromQuery] long userId,
            [FromQuery] Role userRole)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{teamId:long:min(1)}/members/add/{userId:long:min(1)}")]
        public async Task<IActionResult> AddMember(
            long teamId,
            long userId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{teamId:long:min(1)}/members/remove/{userId:long:min(1)}")]
        public async Task<IActionResult> RemoveMember(
            long teamId,
            long userId)
        {
            throw new NotImplementedException();
        }
    }
}
