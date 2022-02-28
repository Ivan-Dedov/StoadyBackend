namespace Stoady.DataAccess.Models.Dao
{
    public sealed class TopicDao : IDao
    {
        public long Id { get; init; }

        public string Title { get; init; }

        public string Description { get; init; }

        public long SubjectId { get; init; }
    }
}
