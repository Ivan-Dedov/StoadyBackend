using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Dao;
using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.User.RegisterUser
{
    public sealed record RegisterUserCommand(
            string Username,
            string Email,
            string Password)
        : IRequest<Unit>;

    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RegisterUserCommandHandler> _logger;

        public RegisterUserCommandHandler(
            ILogger<RegisterUserCommandHandler> logger,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(
            RegisterUserCommand request,
            CancellationToken ct)
        {
            var (username, email, password) = request;

            await _userRepository.AddUser(
                new AddUserParameters
                {
                    Username = username,
                    Email = email,
                    Password = password
                },
                ct);

            _logger.LogInformation($"User with email {email} successfully registered");
            return Unit.Value;
        }
    }
}
