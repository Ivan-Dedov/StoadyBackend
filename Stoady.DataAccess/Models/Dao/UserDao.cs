using System;

namespace Stoady.DataAccess.Models.Dao
{
    public sealed class UserDao
    {
        public long Id { get; init; }

        public string Username { get; init; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

        public string Email { get; init; }

        public string Password { get; init; }

        public int AvatarId { get; init; }
    }
}
