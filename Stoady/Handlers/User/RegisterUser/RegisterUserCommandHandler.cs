using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.DataAccess.Models.Dao;

namespace Stoady.Handlers.User.RegisterUser
{
    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, Unit>
    {
        private const int DefaultAvatarId = 0;

        private readonly StoadyDataContext _context;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(
            StoadyDataContext context,
            ILogger<RegisterUserCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            var (username, email, password) = request;

            if (_context.Users.Any(x => x.Email == email))
            {
                var message = $"User with email = {email} already exists";
                throw new ApplicationException(message);
            }

            await _context.Users.AddAsync(
                new UserDao
                {
                    Username = username,
                    Email = email,
                    Password = password,
                    AvatarId = DefaultAvatarId
                },
                cancellationToken);

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not register user with email = {email}";
                throw new ApplicationException(message);
            }

            _logger.LogInformation($"User with email {email} successfully registered");
            return Unit.Value;
        }
    }

    public sealed record RegisterUserCommand(
            string Username,
            string Email,
            string Password)
        : IRequest<Unit>;
}
