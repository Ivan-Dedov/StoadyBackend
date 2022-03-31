using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.Authentication;
using Stoady.Services.Interfaces;

namespace Stoady.Handlers.Authentication.Authorization
{
    public sealed record AuthorizationCommand(
            string Email,
            string Password)
        : IRequest<AuthorizationResponse>;

    public sealed class AuthorizationCommandHandler
        : IRequestHandler<AuthorizationCommand, AuthorizationResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthorizationCommandHandler> _logger;
        private readonly IPasswordValidatorService _passwordValidatorService;

        private const string Message = "Incorrect e-mail or password.";

        public AuthorizationCommandHandler(
            ILogger<AuthorizationCommandHandler> logger,
            IUserRepository userRepository,
            IPasswordValidatorService passwordValidatorService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _passwordValidatorService = passwordValidatorService;
        }

        public async Task<AuthorizationResponse> Handle(
            AuthorizationCommand request,
            CancellationToken ct)
        {
            var (email, password) = request;

            var user = await _userRepository.GetUserWithPasswordByEmail(email, ct);

            if (user is null)
            {
                _logger.LogWarning(Message);
                throw new ApplicationException(Message);
            }

            if (_passwordValidatorService.ValidatePassword(password, user.Password, user.Salt))
            {
                return new AuthorizationResponse
                {
                    Id = user.Id,
                    Name = user.Username,
                    AvatarId = user.AvatarId
                };
            }

            _logger.LogWarning(Message);
            throw new ApplicationException(Message);
        }
    }
}
