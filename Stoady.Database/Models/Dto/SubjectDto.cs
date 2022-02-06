namespace Stoady.Models.Dto
{
    public sealed class SubjectDto
    {
        public long Id { get; init; }

        public string Title { get; init; }

        public string Image { get; init; }

        public string Description { get; init; }

        public long TeamId { get; init; }
    }
}
