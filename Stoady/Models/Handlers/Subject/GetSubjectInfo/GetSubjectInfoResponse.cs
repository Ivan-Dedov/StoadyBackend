using System.Collections.Generic;

namespace Stoady.Models.Handlers.Subject.GetSubjectInfo
{
    public sealed class GetSubjectInfoResponse
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public List<TopicInSubject> Topics { get; init; }
    }
}
