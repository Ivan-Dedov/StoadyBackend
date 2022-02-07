using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Npgsql;
using Stoady.Database.Models.Dto;
using Stoady.Database.Repositories;
using Stoady.Database.Repositories.Parameters;
using Stoady.Helpers;
using Stoady.Models;
using Stoady.Models.Handlers.Authentication;

namespace Stoady.Handlers.Authentication
{
    public sealed record AuthenticationCommand : IRequest<AuthenticationResponse>
    {
        public string Email { get; init; }

        public string Password { get; init; }
    }

    public sealed class AuthenticationCommandHandler
        : IRequestHandler<AuthenticationCommand, AuthenticationResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthenticationCommandHandler> _logger;

        public AuthenticationCommandHandler(
            IUserRepository userRepository,
            ILogger<AuthenticationCommandHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<AuthenticationResponse> Handle(
            AuthenticationCommand request,
            CancellationToken cancellationToken)
        {
            var user = await GetUser(
                request.Email,
                request.Password,
                cancellationToken);

            return new AuthenticationResponse
            {
                Token = TokenManager.GenerateToken(user.Id),
                User = user
            };
        }

        private async Task<User> GetUser(
            string email,
            string password,
            CancellationToken cancellationToken)
        {
            UserDto userDto;
            try
            {
                userDto = await _userRepository.GetUser(
                    new GetUserParameters
                    {
                        Email = email,
                        Password = password
                    },
                    cancellationToken);
            }
            catch (PostgresException ex)
            {
                _logger.LogWarning(ex.Message);
                throw new ApplicationException("Authentication failed.");
            }

            return new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Username = userDto.Username,
                AvatarId = userDto.AvatarId
            };
        }
    }
}
