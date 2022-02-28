namespace Stoady.Models.Handlers.Authentication
{
    public sealed record AuthenticationResponse(
        string Token,
        string RefreshToken);
}
