namespace Stoady.Models.Data
{
    public sealed class Question
    {
        public long Id { get; init; }

        public string QuestionText { get; init; }

        public string AnswerText { get; init; }

        public long TopicId { get; init; }
    }
}
