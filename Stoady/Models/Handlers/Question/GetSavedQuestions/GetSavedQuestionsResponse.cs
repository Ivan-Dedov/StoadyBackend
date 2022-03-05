using System.Collections.Generic;

namespace Stoady.Models.Handlers.Question.GetSavedQuestions
{
    public sealed class GetSavedQuestionsResponse
    {
        public long UserId { get; init; }

        public List<SavedQuestion> SavedQuestions { get; init; }
    }
}
