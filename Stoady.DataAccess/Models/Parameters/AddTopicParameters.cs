namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class AddTopicParameters
    {
        public long SubjectId { get; init; }

        public string TopicName { get; init; }

        public string TopicDescription { get; init; }
    }
}
