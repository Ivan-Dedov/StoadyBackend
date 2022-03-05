namespace Stoady.DataAccess.Models.Dao
{
    public sealed class StatisticsDao : IDao
    {
        public long Id { get; init; }

        public long UserId { get; init; }

        public long TopicId { get; init; }

        public int Result { get; set; }
    }
}
