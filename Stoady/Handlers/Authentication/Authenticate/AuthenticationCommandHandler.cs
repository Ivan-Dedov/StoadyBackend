using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.Helpers;
using Stoady.Models.Handlers.Authentication;
using Stoady.Services.Interfaces;

namespace Stoady.Handlers.Authentication.Authenticate
{
    public sealed class AuthenticationCommandHandler
        : IRequestHandler<AuthenticationCommand, AuthenticationResponse>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<AuthenticationCommandHandler> _logger;
        private readonly ITokenService _tokenService;
        private readonly IClaimService _claimService;

        public AuthenticationCommandHandler(
            StoadyDataContext context,
            ILogger<AuthenticationCommandHandler> logger,
            ITokenService tokenService,
            IClaimService claimService)
        {
            _context = context;
            _logger = logger;
            _tokenService = tokenService;
            _claimService = claimService;
        }

        public async Task<AuthenticationResponse> Handle(
            AuthenticationCommand request,
            CancellationToken cancellationToken)
        {
            var (username, password) = request;

            var user = _context.Users
                .FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user is null)
            {
                const string message = "Incorrect e-mail or password.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var claims = _claimService.GetClaims(user.Id, username);

            var token = _tokenService.GenerateToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(AuthorizationOptions.RefreshTokenExpirationTimeDays);

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                const string message = "Could not authenticate user.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return new AuthenticationResponse
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }

    public sealed record AuthenticationCommand(
            string Email,
            string Password)
        : IRequest<AuthenticationResponse>;
}
