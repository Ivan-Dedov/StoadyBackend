namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class EditStatisticsParameters
    {
        public long UserId { get; init; }

        public long TopicId { get; init; }

        public int Result { get; init; }
    }
}
