using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

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
        private readonly ILogger<SelectTeamCommandHandler> _logger;

        public SelectTeamCommandHandler(
            IRoleRepository roleRepository,
            ILogger<SelectTeamCommandHandler> logger)
        {
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<SelectTeamResponse> Handle(
            SelectTeamCommand request,
            CancellationToken ct)
        {
            var (userId, teamId) = request;
            var role = await _roleRepository.GetUserRoleByTeamId(userId, teamId, ct);

            if (role is null)
            {
                var message = $"Could not find user with ID = {userId} in team with ID = {teamId}.";
                _logger.LogError(message);
                throw new ApplicationException(message);
            }

            return new SelectTeamResponse
            {
                Role = Enum.Parse<Role>(role.Name)
            };
        }
    }
}
