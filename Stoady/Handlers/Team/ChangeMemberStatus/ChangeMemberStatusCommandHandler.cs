using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.Models;

namespace Stoady.Handlers.Team.ChangeMemberStatus
{
    public sealed class ChangeMemberStatusCommandHandler
        : IRequestHandler<ChangeMemberStatusCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<ChangeMemberStatusCommandHandler> _logger;

        public ChangeMemberStatusCommandHandler(
            StoadyDataContext context,
            ILogger<ChangeMemberStatusCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            ChangeMemberStatusCommand request,
            CancellationToken cancellationToken)
        {
            var (executorId, teamId, userId, role) = request;

            ValidateRights(teamId, executorId);

            var teamUser = _context.TeamUser
                .Where(x => x.TeamId == teamId && x.UserId == userId);

            if (teamUser.Count() != 1)
            {
                var message = $"User (ID = {userId}) is not a member of team with ID = {teamId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            teamUser.First().RoleId = _context.Roles
                .First(x => x.Name == role.ToString())
                .Id;

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not update role for user with ID = {userId} in team with ID = {teamId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }

        private void ValidateRights(long teamId, long userId)
        {
            var users = _context.Users
                .Where(x => x.Id == userId);
            if (users.Count() != 1)
            {
                var message = $"Could not find user with ID = {userId}";
                throw new ApplicationException(message);
            }

            var teams = _context.Teams
                .Where(x => x.Id == teamId);
            if (teams.Count() != 1)
            {
                var message = $"Could not find team with ID = {teamId}";
                throw new ApplicationException(message);
            }

            var teamUsers = _context.TeamUser
                .Where(x => x.TeamId == teamId && x.UserId == userId);
            if (teamUsers.Count() != 1)
            {
                var message = $"Could not find user (ID = {userId}) in team with ID = {teamId}";
                throw new ApplicationException(message);
            }

            var teamUser = teamUsers.First();
            if (_context.Roles.First(x => x.Id == teamUser.RoleId).Name != Role.Admin.ToString())
            {
                var message = $"Access denied for user (ID = {userId}) to modify team (ID = {teamId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }
        }
    }

    public sealed record ChangeMemberStatusCommand(
            long ExecutorId,
            long TeamId,
            long UserId,
            Role Role)
        : IRequest<Unit>;
}
