namespace Stoady.DataAccess.Models.Dao
{
    public sealed class TeamUserDao : IDao
    {
        public long Id { get; init; }

        public long TeamId { get; init; }

        public long UserId { get; init; }

        public long RoleId { get; set; }
    }
}