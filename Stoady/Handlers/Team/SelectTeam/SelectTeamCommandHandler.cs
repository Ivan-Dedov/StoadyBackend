using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;
using Stoady.Models.Handlers.Team.SelectTeam;

namespace Stoady.Handlers.Team.SelectTeam
{
    public sealed record SelectTeamCommand(
            long UserId,
            long TeamId)
        : IRequest<SelectTeamResponse>;

    public sealed class SelectTeamCommandHandler
        : IRequestHandler<SelectTeamCommand, SelectTeamResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public SelectTeamCommandHandler(
            IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<SelectTeamResponse> Handle(
            SelectTeamCommand request,
            CancellationToken ct)
        {
            var (userId, teamId) = request;
            var role = await _roleRepository.GetUserRoleByTeamId(userId, teamId, ct);

            return new SelectTeamResponse
            {
                Role = Enum.Parse<Role>(role.Name)
            };
        }
    }
}
