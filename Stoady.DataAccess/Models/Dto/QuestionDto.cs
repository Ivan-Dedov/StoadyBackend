namespace Stoady.Database.Models.Dto
{
    public sealed class QuestionDto
    {
        public long Id { get; init; }

        public string Question { get; init; }

        public string Answer { get; init; }

        public long TopicId { get; init; }
    }
}
