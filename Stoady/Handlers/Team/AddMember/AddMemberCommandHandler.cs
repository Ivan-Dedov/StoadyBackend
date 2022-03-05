using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.DataAccess.Models.Dao;
using Stoady.Models;

namespace Stoady.Handlers.Team.AddMember
{
    public sealed class AddMemberCommandHandler
        : IRequestHandler<AddMemberCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<AddMemberCommandHandler> _logger;

        public AddMemberCommandHandler(
            StoadyDataContext context,
            ILogger<AddMemberCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            AddMemberCommand request,
            CancellationToken cancellationToken)
        {
            var (executorId, teamId, email) = request;

            ValidateRights(teamId, executorId);

            var users = _context.Users
                .Where(x => x.Email == email);
            if (users.Count() != 1)
            {
                var message = $"Could not find user with email = {email}";
                throw new ApplicationException(message);
            }

            var user = users.First();

            if (_context.TeamUser.Any(x => x.TeamId == teamId && x.UserId == user.Id))
            {
                var message = $"User with email = {email} is already a member of team with ID = {teamId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            await _context.TeamUser.AddAsync(
                new TeamUserDao
                {
                    TeamId = teamId,
                    UserId = user.Id,
                    RoleId = _context.Roles
                        .First(x => x.Name == Role.Member.ToString())
                        .Id
                },
                cancellationToken);

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not add user (ID = {user.Id}) to team with ID = {teamId}";
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

    public sealed record AddMemberCommand(
            long ExecutorId,
            long TeamId,
            string Email)
        : IRequest<Unit>;
}
