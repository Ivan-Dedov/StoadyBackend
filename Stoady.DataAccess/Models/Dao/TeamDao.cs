namespace Stoady.DataAccess.Models.Dao
{
    public sealed class TeamDao : IDao
    {
        public long Id { get; init; }

        public string Name { get; set; }

        public string Avatar { get; set; }
    }
}
