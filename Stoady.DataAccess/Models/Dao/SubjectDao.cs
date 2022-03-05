namespace Stoady.DataAccess.Models.Dao
{
    public sealed class SubjectDao : IDao
    {
        public long Id { get; init; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public long TeamId { get; init; }
    }
}
