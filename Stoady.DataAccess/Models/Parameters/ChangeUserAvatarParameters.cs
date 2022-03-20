namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class ChangeUserAvatarParameters
    {
        public long UserId { get; init; }

        public int AvatarId { get; init; }
    }
}
