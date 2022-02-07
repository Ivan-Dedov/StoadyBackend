using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Stoady.Models;

namespace Stoady.Helpers
{
    public static class TokenManager
    {
        public static string GenerateToken(
            long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GetClaims(userId),
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

        public static string GenerateTokenWithTeam(
            long userId,
            long teamId,
            Role role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GetClaims(userId, teamId, role),
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

        private static ClaimsIdentity GetClaims(
            long userId,
            long? teamId = null,
            Role? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomJwtClaims.Id, userId.ToString())
            };

            if (teamId.HasValue)
            {
                claims.Add(new Claim(CustomJwtClaims.TeamId, teamId.Value.ToString()));
            }

            if (role.HasValue)
            {
                claims.Add(new Claim(CustomJwtClaims.Role, role.Value.ToString()));
            }

            return new ClaimsIdentity(claims);
        }
    }
}
