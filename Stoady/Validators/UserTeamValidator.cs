using System.Linq;

using Stoady.DataAccess.DataContexts;
using Stoady.Models;
using Stoady.Validators.Interfaces;

namespace Stoady.Validators
{
    public sealed class UserTeamValidator : IUserTeamValidator
    {
        private readonly StoadyDataContext _context;
        private readonly ITeamValidator _teamValidator;
        private readonly IUserValidator _userValidator;

        public UserTeamValidator(
            StoadyDataContext context,
            ITeamValidator teamValidator,
            IUserValidator userValidator)
        {
            _context = context;
            _teamValidator = teamValidator;
            _userValidator = userValidator;
        }

        /// <inheritdoc/>
        public bool ValidateUserIsInTeam(
            long userId,
            long teamId)
        {
            return _userValidator.ValidateUser(userId)
                   && _teamValidator.ValidateTeam(teamId)
                   && _context.TeamUser.Count(x => x.TeamId == teamId && x.UserId == userId) == 1;
        }

        /// <inheritdoc/>
        public bool ValidateUserHasAdminRights(
            long userId,
            long teamId)
        {
            var adminRoleId = _context.Roles
                .First(x => x.Name == Role.Admin.ToString()).Id;

            return ValidateUserIsInTeam(userId, teamId)
                   && _context.TeamUser
                       .First(x => x.TeamId == teamId && x.UserId == userId)
                       .RoleId == adminRoleId;
        }
    }
}
