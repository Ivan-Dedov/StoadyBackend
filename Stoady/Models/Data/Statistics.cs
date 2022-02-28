namespace Stoady.Models.Data
{
    public sealed class Statistics
    {
        public long Id { get; init; }

        public long UserId { get; init; }

        public long TopicId { get; init; }

        public int Result { get; init; }
    }
}
