namespace Stoady.Models.Handlers.Authentication
{
    public sealed record AuthorizationRequest(
        string Email,
        string Password);
}
