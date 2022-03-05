namespace Stoady.DataAccess.Models.Dao
{
    public sealed class UserDao : IDao
    {
        public long Id { get; init; }

        public string Username { get; init; }

        public string Email { get; init; }

        public string Password { get; set; }

        public int AvatarId { get; set; }
    }
}