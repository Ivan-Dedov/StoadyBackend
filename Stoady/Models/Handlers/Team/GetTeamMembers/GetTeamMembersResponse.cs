using System.Collections.Generic;

namespace Stoady.Models.Handlers.Team.GetTeamMembers
{
    public sealed class GetTeamMembersResponse
    {
        public List<MemberInTeam> Members { get; init; }
    }
}
