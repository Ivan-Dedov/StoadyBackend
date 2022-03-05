namespace Stoady.Models.Handlers.Authentication
{
    public sealed record AuthenticationRequest(
        string Email,
        string Password);
}
