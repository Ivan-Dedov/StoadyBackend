namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class AddQuestionParameters
    {
        public long TopicId { get; init; }

        public string QuestionText { get; init; }

        public string AnswerText { get; init; }
    }
}
