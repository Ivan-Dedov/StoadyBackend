namespace Stoady.Database.Models.Dto
{
    public sealed class UserDto
    {
        public long Id { get; init; }

        public string Username { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }

        public int AvatarId { get; init; }
    }
}
