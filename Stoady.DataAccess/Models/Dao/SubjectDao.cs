namespace Stoady.DataAccess.Models.Dao
{
    public sealed class SubjectDao
    {
        public long Id { get; init; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long TeamId { get; init; }
    }
}
