namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class ChangeMemberStatusParameters
    {
        public long TeamId { get; init; }

        public long UserId { get; init; }

        public long RoleId { get; init; }
    }
}
