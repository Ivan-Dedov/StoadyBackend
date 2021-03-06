namespace Stoady.DataAccess.Models.Dao
{
    public sealed class UserDao
    {
        public long Id { get; init; }

        public string Username { get; init; }

        public string Email { get; init; }

        public int AvatarId { get; init; }
    }
}
