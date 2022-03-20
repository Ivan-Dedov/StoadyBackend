namespace Stoady.Models.Handlers.Authentication
{
    public sealed class AuthenticationResponse
    {
        public string Token { get; init; }

        public string RefreshToken { get; init; }
    }
}
