namespace Stoady.DataAccess.Models.Dao
{
    public sealed class TopicDao
    {
        public long Id { get; init; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long SubjectId { get; init; }
    }
}
