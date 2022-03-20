using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;
using Stoady.Models.Handlers.Team.GetTeamMembers;

namespace Stoady.Handlers.Team.GetTeamMembers
{
    public sealed record GetTeamMembersCommand(
            long TeamId)
        : IRequest<GetTeamMembersResponse>;

    public sealed class GetTeamMembersCommandHandler
        : IRequestHandler<GetTeamMembersCommand, GetTeamMembersResponse>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<GetTeamMembersCommandHandler> _logger;

        public GetTeamMembersCommandHandler(
            ILogger<GetTeamMembersCommandHandler> logger,
            ITeamRepository teamRepository,
            IRoleRepository roleRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
            _roleRepository = roleRepository;
        }

        public async Task<GetTeamMembersResponse> Handle(
            GetTeamMembersCommand request,
            CancellationToken ct)
        {
            var teamId = request.TeamId;

            var usersInTeamTasks = (await _teamRepository
                    .GetTeamMembersByTeamId(teamId, ct))
                .Select(async x =>
                    new MemberInTeam
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Email = x.Email,
                        Role = Enum.Parse<Role>((await _roleRepository.GetUserRoleByTeamId(x.Id, teamId, ct)).Name)
                    })
                .ToList();

            await Task.WhenAll(usersInTeamTasks);

            var members = usersInTeamTasks
                .Select(x => x.Result)
                .ToList();

            return new GetTeamMembersResponse
            {
                Members = members
            };
        }
    }
}
