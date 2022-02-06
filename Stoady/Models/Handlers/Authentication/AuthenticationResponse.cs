namespace Stoady.Models.Handlers.Authentication
{
    public sealed record AuthenticationResponse
    {
        public string Token { get; init; }

        public User User { get; init; }
    }
}
