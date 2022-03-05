using System.Collections.Generic;

namespace Stoady.Models.Handlers.Question.GetQuestions
{
    public sealed class GetQuestionsResponse
    {
        public List<QuestionInTopic> Questions { get; init; }
    }
}
