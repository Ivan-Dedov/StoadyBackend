namespace Stoady.Models.Handlers.Authentication
{
    public sealed class AuthenticationRequest
    {
        public string Email { get; init; }

        public string Password { get; init; }
    }
}
