using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.Authentication;

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

        public AuthorizationCommandHandler(
            ILogger<AuthorizationCommandHandler> logger,
            IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<AuthorizationResponse> Handle(
            AuthorizationCommand request,
            CancellationToken ct)
        {
            var (email, password) = request;

            var user = await _userRepository.GetUserByEmail(email, ct);

            if (user is null)
            {
                const string message = "Incorrect e-mail or password.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var dbPasswordInfo = await _userRepository.GetUserPasswordAndSalt(user.Id, ct);

            const string localSalt = "HSECourseWorkStoady";
            var saltBytes = Convert.FromBase64String(dbPasswordInfo.Salt);
            var passwordStr = Encoding.UTF8.GetBytes(password)
                .Concat(saltBytes)
                .Concat(Encoding.UTF8.GetBytes(localSalt));

            var hashAlgorithm = SHA256.Create();
            var hashedPassword = Convert.ToBase64String(
                hashAlgorithm.ComputeHash(passwordStr.ToArray()));

            if (hashedPassword != dbPasswordInfo.Password)
            {
                const string message = "Incorrect e-mail or password.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return new AuthorizationResponse
            {
                Id = user.Id,
                Name = user.Username,
                AvatarId = user.AvatarId
            };
        }
    }
}
