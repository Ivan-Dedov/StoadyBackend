namespace Stoady.Models.Handlers.User.RegisterUser
{
    public sealed record RegisterUserRequest(
        string Username,
        string Email,
        string Password);
}
