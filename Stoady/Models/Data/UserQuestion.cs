namespace Stoady.Models.Data
{
    public sealed class UserQuestion
    {
        public long Id { get; init; }

        public long UserId { get; init; }

        public long QuestionId { get; init; }
    }
}
