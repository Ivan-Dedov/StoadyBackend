namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class AddUserParameters
    {
        public string Username { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public string Salt { get; init; }

        public int AvatarId { get; init; }
    }
}
