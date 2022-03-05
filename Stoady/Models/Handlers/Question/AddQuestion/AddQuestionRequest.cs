namespace Stoady.Models.Handlers.Question.AddQuestion
{
    public sealed record AddQuestionRequest(
        long TopicId,
        string QuestionText,
        string AnswerText);
}
