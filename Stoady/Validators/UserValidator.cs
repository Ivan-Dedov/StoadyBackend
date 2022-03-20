using System.Linq;

using Stoady.DataAccess.DataContexts;
using Stoady.Validators.Interfaces;

namespace Stoady.Validators
{
    public sealed class UserValidator : IUserValidator
    {
        private readonly StoadyDataContext _context;

        public UserValidator(
            StoadyDataContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public bool ValidateUser(
            long userId)
        {
            return _context.Users.Count(x => x.Id == userId) == 1;
        }
    }
}
