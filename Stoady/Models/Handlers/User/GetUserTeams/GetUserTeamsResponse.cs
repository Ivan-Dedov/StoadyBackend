using System.Collections.Generic;

namespace Stoady.Models.Handlers.User.GetUserTeams
{
    public sealed class GetUserTeamsResponse
    {
        public List<UserTeam> Teams { get; init; }
    }
}
