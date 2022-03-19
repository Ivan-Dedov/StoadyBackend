using System.Collections.Generic;
using System.Security.Claims;

using Stoady.Models;

namespace Stoady.Services.Interfaces
{
    public interface IClaimService
    {
        IEnumerable<Claim> GetClaims(
            long userId,
            string userName);

        IEnumerable<Claim> GetClaims(
            long userId,
            string userName,
            long teamId,
            Role role);
    }
}
