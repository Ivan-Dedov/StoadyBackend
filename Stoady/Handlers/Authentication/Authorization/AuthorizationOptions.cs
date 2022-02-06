using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Stoady.Handlers.Authentication.Authorization
{
    public static class AuthorizationOptions
    {
        private const string Key = "assdinsan_stoady_hse_cs_2022";

        public const string Issuer = "StoadyIvan";
        public const string Audience = "StoadyAnna";
        public const int ExpirationTimeHours = 12;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
