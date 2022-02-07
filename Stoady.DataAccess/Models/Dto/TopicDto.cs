namespace Stoady.Database.Models.Dto
{
    public sealed class TopicDto
    {
        public long Id { get; init; }

        public string Description { get; init; }

        public long SubjectId { get; init; }
    }
}
