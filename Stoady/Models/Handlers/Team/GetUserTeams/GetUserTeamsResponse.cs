using System.Collections.Generic;

namespace Stoady.Models.Handlers.Team.GetUserTeams
{
    public sealed class GetUserTeamsResponse
    {
        public List<UserTeam> Teams { get; init; }
    }
}
