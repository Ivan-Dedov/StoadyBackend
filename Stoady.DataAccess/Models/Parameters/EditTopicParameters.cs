namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class EditTopicParameters
    {
        public long TopicId { get; init; }

        public string TopicName { get; init; }

        public string TopicDescription { get; init; }
    }
}
