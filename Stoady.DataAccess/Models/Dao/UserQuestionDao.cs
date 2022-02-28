namespace Stoady.DataAccess.Models.Dao
{
    public sealed class UserQuestionDao : IDao
    {
        public long Id { get; init; }

        public long UserId { get; init; }

        public long QuestionId { get; init; }
    }
}
