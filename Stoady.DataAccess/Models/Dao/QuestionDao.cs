namespace Stoady.DataAccess.Models.Dao
{
    public sealed class QuestionDao : IDao
    {
        public long Id { get; init; }

        public string QuestionText { get; init; }

        public string AnswerText { get; init; }

        public long TopicId { get; init; }
    }
}
