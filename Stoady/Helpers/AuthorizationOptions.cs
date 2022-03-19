using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Stoady.Helpers
{
    public static class AuthorizationOptions
    {
        public const string Secret = "HSE-SE-2022-Stoady";

        public const string Issuer = "StoadyIvan";
        public const string Audience = "StoadyAnna";

        public const int TokenExpirationTimeMinutes = 5;
        public const int RefreshTokenExpirationTimeDays = 7;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new (Encoding.ASCII.GetBytes(Secret));
        }
    }
}
