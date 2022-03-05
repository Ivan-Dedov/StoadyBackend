namespace Stoady.Models.Handlers.Topic.EditTopic
{
    public sealed record EditTopicRequest(
        string TopicName,
        string TopicDescription);
}
