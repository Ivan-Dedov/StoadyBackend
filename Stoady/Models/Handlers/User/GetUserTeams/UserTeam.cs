namespace Stoady.Models.Handlers.User.GetUserTeams
{
    public sealed class UserTeam
    {
        public long TeamId { get; init; }

        public Role Role { get; init; }

        public string TeamName { get; init; }

        public string TeamAvatar { get; init; }
    }
}
