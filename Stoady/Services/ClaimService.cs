using System.Collections.Generic;
using System.Security.Claims;

using Stoady.Helpers;
using Stoady.Models;
using Stoady.Services.Interfaces;

namespace Stoady.Services
{
    public sealed class ClaimService : IClaimService
    {
        public IEnumerable<Claim> GetClaims(
            long userId,
            string userName)
        {
            return new List<Claim>
            {
                new Claim(CustomJwtClaims.Id, userId.ToString()),
                new Claim(CustomJwtClaims.Name, userName)
            };
        }

        public IEnumerable<Claim> GetClaims(
            long userId,
            string userName,
            long teamId,
            Role role)
        {
            return new List<Claim>
            {
                new Claim(CustomJwtClaims.Id, userId.ToString()),
                new Claim(CustomJwtClaims.Name, userName),
                new Claim(CustomJwtClaims.TeamId, teamId.ToString()),
                new Claim(CustomJwtClaims.Role, role.ToString())
            };
        }
    }
}
