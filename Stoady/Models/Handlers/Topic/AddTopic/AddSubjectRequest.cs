namespace Stoady.Models.Handlers.Topic.AddTopic
{
    public sealed record AddTopicRequest(
        long SubjectId,
        string TopicName,
        string TopicDescription);
}
