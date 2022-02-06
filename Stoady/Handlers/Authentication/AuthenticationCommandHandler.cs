using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using Stoady.Handlers.Authentication.Authorization;
using Stoady.Models;
using Stoady.Models.Handlers.Authentication;
using Stoady.Repositories;
using Stoady.Repositories.Parameters;

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
                Token = GenerateToken(user.Id),
                User = user
            };
        }

        private async Task<User> GetUser(
            string email,
            string password,
            CancellationToken cancellationToken)
        {
            try
            {
                return await _userRepository.GetUser(
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
                throw new BadHttpRequestException("Failed to authenticate: User Not Found.");
            }
        }

        private static string GenerateToken(
            long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(CustomJwtClaim.Id, userId.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(AuthorizationOptions.ExpirationTimeHours),
                Issuer = AuthorizationOptions.Issuer,
                Audience = AuthorizationOptions.Audience,
                SigningCredentials = new SigningCredentials(
                    AuthorizationOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
