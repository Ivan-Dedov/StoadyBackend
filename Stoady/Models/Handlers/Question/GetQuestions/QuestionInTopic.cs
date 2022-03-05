namespace Stoady.Models.Handlers.Question.GetQuestions
{
    public sealed class QuestionInTopic
    {
        public long Id { get; init; }

        public string QuestionText { get; init; }

        public string AnswerText { get; init; }
    }
}
