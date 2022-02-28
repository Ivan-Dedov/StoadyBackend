namespace Stoady.Models.Handlers.Topic.EditTopic
{
    public sealed record EditTopicRequest(
        long TopicId,
        string TopicName,
        string TopicDescription);
}
