namespace Stoady.Models.Handlers.Question.EditQuestion
{
    public sealed record EditQuestionRequest(
        long TopicId,
        string QuestionText,
        string AnswerText);
}
