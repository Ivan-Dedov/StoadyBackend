using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.DataAccess.Models.Dao;

using Role = Stoady.Models.Role;

namespace Stoady.Handlers.Team.CreateTeam
{
    public sealed class CreateTeamCommandHandler
        : IRequestHandler<CreateTeamCommand, Unit>
    {
        private const string DefaultTeamAvatar = "";

        private readonly StoadyDataContext _context;
        private readonly ILogger<CreateTeamCommandHandler> _logger;

        public CreateTeamCommandHandler(
            StoadyDataContext context,
            ILogger<CreateTeamCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            CreateTeamCommand request,
            CancellationToken cancellationToken)
        {
            var (userId, teamName) = request;

            var newTeam = new TeamDao
            {
                Name = teamName,
                Avatar = DefaultTeamAvatar,
            };

            await _context.Teams.AddAsync(
                newTeam,
                cancellationToken);

            await _context.TeamUser.AddAsync(
                new TeamUserDao
                {
                    TeamId = newTeam.Id,
                    UserId = userId,
                    RoleId = _context.Roles.First(x => x.Name == Role.Creator.ToString()).Id
                },
                cancellationToken);

            if (await _context.SaveChangesAsync() != 2)
            {
                var message = $"Could not create team for user with ID = {userId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record CreateTeamCommand(
            long UserId,
            string TeamName)
        : IRequest<Unit>;
}
