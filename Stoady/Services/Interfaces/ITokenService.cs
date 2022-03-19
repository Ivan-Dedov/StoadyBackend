using System.Collections.Generic;
using System.Security.Claims;

namespace Stoady.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
