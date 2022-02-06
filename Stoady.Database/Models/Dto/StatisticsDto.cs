namespace Stoady.Models.Dto
{
    public sealed class StatisticsDto
    {
        public long Id { get; init; }

        public long UserId { get; init; }

        public long TopicId { get; init; }

        public int Result { get; init; }
    }
}
