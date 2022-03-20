using System.Linq;

using Stoady.DataAccess.DataContexts;
using Stoady.Validators.Interfaces;

namespace Stoady.Validators
{
    public sealed class TeamValidator : ITeamValidator
    {
        private readonly StoadyDataContext _context;

        public TeamValidator(
            StoadyDataContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public bool ValidateTeam(
            long teamId)
        {
            return _context.Teams.Count(x => x.Id == teamId) == 1;
        }
    }
}
