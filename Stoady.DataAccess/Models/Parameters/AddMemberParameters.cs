namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class AddMemberParameters
    {
        public long RoleId { get; init; }

        public long TeamId { get; init; }

        public long UserId { get; init; }
    }
}
