namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class EditQuestionParameters
    {
        public long QuestionId { get; init; }

        public string QuestionText { get; init; }

        public string AnswerText { get; init; }
    }
}
