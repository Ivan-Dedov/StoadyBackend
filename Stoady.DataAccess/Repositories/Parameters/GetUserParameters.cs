namespace Stoady.Database.Repositories.Parameters
{
    public sealed record GetUserParameters
    {
        public string Email { get; init; }

        public string Password { get; init; }
    }
}
