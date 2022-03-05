namespace Stoady.Models.Handlers.Team.GetTeamMembers
{
    public sealed class MemberInTeam
    {
        public long Id { get; init; }

        public string Username { get; init; }

        public string Email { get; init; }

        public Role Role { get; init; }
    }
}
