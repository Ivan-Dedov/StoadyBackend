using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.User.RegisterUser;
using Stoady.Services.Interfaces;

namespace Stoady.Handlers.User.RegisterUser
{
    public sealed record RegisterUserCommand(
            string Username,
            string Email,
            string Password,
            int AvatarId)
        : IRequest<RegisterUserResponse>;

    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        private readonly IPasswordValidatorService _passwordValidatorService;

        public RegisterUserCommandHandler(
            ILogger<RegisterUserCommandHandler> logger,
            IUserRepository userRepository,
            IPasswordValidatorService passwordValidatorService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordValidatorService = passwordValidatorService;
        }

        public async Task<RegisterUserResponse> Handle(
            RegisterUserCommand request,
            CancellationToken ct)
        {
            var (username, email, password, avatarId) = request;

            if (await CheckUserAlreadyExists(email, ct))
            {
                throw new ApplicationException("User already exists");
            }

            var (hashedPassword, salt) = _passwordValidatorService.GetHashedPassword(password);

            var result = await _userRepository.AddUser(
                new AddUserParameters
                {
                    Username = username,
                    Email = email,
                    Password = hashedPassword,
                    Salt = salt,
                    AvatarId = avatarId
                },
                ct);

            if (result != 1)
            {
                const string message = "Something went wrong when registering. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            _logger.LogInformation($"User with email {email} successfully registered");

            try
            {
                return new RegisterUserResponse
                {
                    UserId = (await _userRepository.GetUserByEmail(email, ct)).Id
                };
            }
            catch (Exception ex)
            {
                var message = "Something went wrong when registering." + Environment.NewLine + ex.Message;
                _logger.LogError(message);
                throw new ApplicationException(message);
            }
        }

        private async Task<bool> CheckUserAlreadyExists(
            string email,
            CancellationToken ct)
        {
            var user = await _userRepository.GetUserByEmail(email, ct);
            return user is not null;
        }
    }
}
