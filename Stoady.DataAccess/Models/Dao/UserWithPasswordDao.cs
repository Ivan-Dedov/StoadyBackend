namespace Stoady.DataAccess.Models.Dao
{
    public sealed class UserWithPasswordDao
    {
        public long Id { get; init; }

        public string Username { get; init; }

        public string Email { get; init; }

        public int AvatarId { get; init; }

        public string Password { get; init; }

        public string Salt { get; init; }
    }
}
