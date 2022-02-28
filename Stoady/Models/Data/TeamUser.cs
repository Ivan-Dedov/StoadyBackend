namespace Stoady.Models.Data
{
    public sealed class TeamUser
    {
        public long Id { get; init; }

        public long TeamId { get; init; }

        public long UserId { get; init; }

        public long RoleId { get; init; }
    }
}
