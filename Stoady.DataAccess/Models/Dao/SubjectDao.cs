namespace Stoady.DataAccess.Models.Dao
{
    public sealed class SubjectDao : IDao
    {
        public long Id { get; init; }

        public string Title { get; init; }

        public string Image { get; init; }

        public string Description { get; init; }

        public long TeamId { get; init; }
    }
}
