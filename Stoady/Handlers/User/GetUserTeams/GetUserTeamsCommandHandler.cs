using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;
using Stoady.Models.Handlers.User.GetUserTeams;

namespace Stoady.Handlers.User.GetUserTeams
{
    public sealed record GetUserTeamsCommand(
            long UserId)
        : IRequest<GetUserTeamsResponse>;

    public sealed class GetUserTeamsCommandHandler
        : IRequestHandler<GetUserTeamsCommand, GetUserTeamsResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<GetUserTeamsCommandHandler> _logger;

        public GetUserTeamsCommandHandler(
            ILogger<GetUserTeamsCommandHandler> logger,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<GetUserTeamsResponse> Handle(
            GetUserTeamsCommand request,
            CancellationToken ct)
        {
            var userId = request.UserId;

            var teams = await _userRepository.GetTeamsByUserId(userId, ct);

            var userTeamsTasks = teams
                .Select(async x =>
                    new UserTeam
                    {
                        TeamId = x.Id,
                        Role = Enum.Parse<Role>((await _roleRepository.GetUserRoleByTeamId(userId, x.Id, ct)).Name),
                        TeamName = x.Name,
                        TeamAvatar = x.Avatar
                    })
                .ToList();

            await Task.WhenAll(userTeamsTasks);

            var result = userTeamsTasks.Select(x => x.Result).ToList();

            return new GetUserTeamsResponse
            {
                Teams = result
            };
        }
    }
}
