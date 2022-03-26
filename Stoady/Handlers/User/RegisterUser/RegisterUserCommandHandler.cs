using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

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

            if (await CheckUserAlreadyExists(email, ct))
            {
                throw new ApplicationException("User already exists");
            }

            const string localSalt = "HSECourseWorkStoady";

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = GenerateSalt();
            var localSaltBytes = Encoding.UTF8.GetBytes(localSalt);

            var passwordWithSalt = passwordBytes
                .Concat(saltBytes)
                .Concat(localSaltBytes);
            var hashAlgorithm = SHA256.Create();

            var digestBytes = hashAlgorithm.ComputeHash(passwordWithSalt.ToArray());
            var stringSalt = Convert.ToBase64String(saltBytes);
            var hashedPassword = Convert.ToBase64String(digestBytes);

            await _userRepository.AddUser(
                new AddUserParameters
                {
                    Username = username,
                    Email = email,
                    Password = hashedPassword,
                    Salt = stringSalt
                },
                ct);

            _logger.LogInformation($"User with email {email} successfully registered");
            return Unit.Value;
        }

        private async Task<bool> CheckUserAlreadyExists(
            string email,
            CancellationToken ct)
        {
            var user = await _userRepository.GetUserByEmail(email, ct);
            return user is not null;
        }

        private static byte[] GenerateSalt()
        {
            const int saltLength = 32;
            return RandomNumberGenerator.GetBytes(saltLength);
        }
    }
}
