using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Stoady.DataAccess.DataContexts;
using Stoady.Models;
using Stoady.Services.Interfaces;

namespace Stoady.Controllers
{
    /// <summary>
    /// Контроль токенов
    /// </summary>
    [Route("tokens")]
    [ApiController]
    public sealed class TokenController : ControllerBase
    {
        private readonly StoadyDataContext _context;
        private readonly ITokenService _tokenService;

        public TokenController(
            StoadyDataContext context,
            ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Обновляет токен пользователя
        /// </summary>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(
            TokenModel token,
            CancellationToken cancellationToken)
        {
            if (token is null)
            {
                return BadRequest("Invalid client request");
            }

            var (accessToken, refreshToken) = token;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var user = _context.Users.SingleOrDefault(x => x.Username == principal.Identity.Name);

            if (user is null
                || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }

            var newAccessToken = _tokenService.GenerateToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new ApplicationException("Could not refresh");
            }

            return new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }

        /// <summary>
        /// Отменяет токен пользователя
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke(
            CancellationToken cancellationToken)
        {
            var username = User.Identity?.Name;
            var user = _context.Users.SingleOrDefault(x => x.Username == username);
            if (user == null)
            {
                return BadRequest();
            }

            user.RefreshToken = null;
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new ApplicationException("Failed to revoke");
            }

            return Ok();
        }
    }
}
