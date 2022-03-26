using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

using Microsoft.IdentityModel.Tokens;

using Stoady.Helpers;
using Stoady.Services.Interfaces;

namespace Stoady.Services
{
    public sealed class TokenService : ITokenService
    {
        public string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = AuthorizationOptions.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                AuthorizationOptions.Issuer,
                AuthorizationOptions.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(AuthorizationOptions.TokenExpirationTimeMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public string GenerateRefreshToken()
        {
            var randomArray = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(randomArray);
            return Convert.ToBase64String(randomArray);
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, // todo change to true?
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthorizationOptions.GetSymmetricSecurityKey(),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || jwtSecurityToken.Header.Alg
                        .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)
                    is false)
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
