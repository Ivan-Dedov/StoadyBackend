namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class EditTeamParameters
    {
        public long TeamId { get; init; }

        public string TeamName { get; init; }

        public string TeamAvatar { get; init; }
    }
}
