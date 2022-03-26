namespace Stoady.DataAccess.Models.Dao
{
    public sealed class StatisticsDao
    {
        public long Id { get; init; }

        public long UserId { get; init; }

        public long TopicId { get; init; }

        public string TopicName { get; init; }

        public int Result { get; set; }
    }
}
