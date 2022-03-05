namespace Stoady.Models.Handlers.Question.GetSavedQuestions
{
    public sealed class SavedQuestion
    {
        public long Id { get; init; }

        public string QuestionText { get; init; }

        public string AnswerText { get; init; }

        public long TopicId { get; init; }
    }
}
