using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models;

namespace Stoady.Handlers.Team.CreateTeam
{
    public sealed record CreateTeamCommand(
            long UserId,
            string TeamName)
        : IRequest<Unit>;

    public sealed class CreateTeamCommandHandler
        : IRequestHandler<CreateTeamCommand, Unit>
    {
        private const string DefaultTeamAvatar = "";

        private readonly ITeamRepository _teamRepository;
        private readonly ILogger<CreateTeamCommandHandler> _logger;

        public CreateTeamCommandHandler(
            ILogger<CreateTeamCommandHandler> logger,
            ITeamRepository teamRepository,
            IRoleRepository roleRepository)
        {
            _logger = logger;
            _teamRepository = teamRepository;
        }

        public async Task<Unit> Handle(
            CreateTeamCommand request,
            CancellationToken ct)
        {
            var (userId, teamName) = request;

            await _teamRepository.CreateTeam(
                new CreateTeamParameters
                {
                    TeamName = teamName,
                    Avatar = DefaultTeamAvatar
                },
                ct);

            await _teamRepository.AddMember(
                new AddMemberParameters
                {
                    RoleId = (long)Role.Creator,
                    TeamId = /*todo how to determine ID*/0,
                    UserId = userId
                },
                ct);

            return Unit.Value;
        }
    }
}
