namespace Stoady.DataAccess.Models.Dao
{
    public sealed class QuestionDao
    {
        public long Id { get; init; }

        public string QuestionText { get; set; }

        public string AnswerText { get; set; }

        public long TopicId { get; init; }
    }
}
