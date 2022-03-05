using System.Collections.Generic;

namespace Stoady.Models.Handlers.Topic.GetTopicInfo
{
    public sealed class GetTopicInfoResponse
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public List<QuestionInTopicInfo> Questions { get; init; }
    }
}
