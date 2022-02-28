namespace Stoady.Models.Handlers.User.RegisterUser
{
    public sealed record RegisterUserRequest(
        string Name,
        string Email,
        string Password);
}
