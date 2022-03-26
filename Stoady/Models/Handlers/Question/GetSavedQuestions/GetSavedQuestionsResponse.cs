using System.Collections.Generic;

namespace Stoady.Models.Handlers.Question.GetSavedQuestions
{
    public sealed class GetSavedQuestionsResponse
    {
        public List<SavedQuestion> SavedQuestions { get; init; }
    }
}
