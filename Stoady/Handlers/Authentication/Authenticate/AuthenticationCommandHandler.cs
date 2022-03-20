using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Helpers;
using Stoady.Models.Handlers.Authentication;
using Stoady.Services.Interfaces;

namespace Stoady.Handlers.Authentication.Authenticate
{
    public sealed record AuthenticationCommand(
            string Email,
            string Password)
        : IRequest<AuthenticationResponse>;

    public sealed class AuthenticationCommandHandler
        : IRequestHandler<AuthenticationCommand, AuthenticationResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthenticationCommandHandler> _logger;
        private readonly ITokenService _tokenService;
        private readonly IClaimService _claimService;

        public AuthenticationCommandHandler(
            ILogger<AuthenticationCommandHandler> logger,
            ITokenService tokenService,
            IClaimService claimService,
            IUserRepository userRepository)
        {
            _logger = logger;
            _tokenService = tokenService;
            _claimService = claimService;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResponse> Handle(
            AuthenticationCommand request,
            CancellationToken ct)
        {
            var (email, password) = request;

            var user = await _userRepository.GetUser(email, password, ct);

            if (user is null)
            {
                const string message = "Incorrect e-mail or password.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var claims = _claimService.GetClaims(user.Id, email);

            var token = _tokenService.GenerateToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(AuthorizationOptions.RefreshTokenExpirationTimeDays);

            return new AuthenticationResponse
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }
}
