namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class ChangeTeamAvatarParameters
    {
        public long TeamId { get; init; }

        public string TeamAvatar { get; init; }
    }
}
