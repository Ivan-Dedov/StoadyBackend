namespace Stoady.Models
{
    public sealed class User
    {
        public long Id { get; init; }

        public string Username { get; init; }

        public string Email { get; init; }

        public int AvatarId { get; init; }
    }
}
