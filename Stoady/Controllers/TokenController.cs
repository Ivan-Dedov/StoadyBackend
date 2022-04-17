using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly ITokenService _tokenService;

        public TokenController(
            ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        /// <summary>
        /// Обновляет токен пользователя
        /// </summary>
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(
            TokenModel token,
            CancellationToken ct)
        {
            // TODO Перейти на новую авторизацию
            throw new NotImplementedException();
        }

        /// <summary>
        /// Отменяет токен пользователя
        /// </summary>
        [Authorize]
        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke(
            CancellationToken ct)
        {
            // TODO Перейти на новую авторизацию
            throw new NotImplementedException();
        }
    }
}
