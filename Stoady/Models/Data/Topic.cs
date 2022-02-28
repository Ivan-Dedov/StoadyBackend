namespace Stoady.Models.Data
{
    public sealed class Topic
    {
        public long Id { get; init; }

        public string Title { get; init; }

        public string Description { get; init; }

        public long SubjectId { get; init; }
    }
}
