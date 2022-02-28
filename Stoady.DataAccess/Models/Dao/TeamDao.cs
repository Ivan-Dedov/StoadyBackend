namespace Stoady.DataAccess.Models.Dao
{
    public sealed class TeamDao : IDao
    {
        public long Id { get; init; }

        public string Name { get; init; }

        public string Avatar { get; init; }
    }
}
