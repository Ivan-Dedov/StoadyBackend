using System.Collections.Generic;

namespace Stoady.Models.Handlers.Team.GetTeamInfo
{
    public sealed class GetTeamInfoResponse
    {
        public string Name { get; init; }

        public string Picture { get; init; }

        public List<SubjectInTeam> Subjects { get; init; }
    }
}
