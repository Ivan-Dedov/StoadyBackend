namespace Stoady.DataAccess.Models.Parameters
{
    public sealed class RemoveQuestionFromSavedParameters
    {
        public long UserId { get; init; }

        public long QuestionId { get; init; }
    }
}
