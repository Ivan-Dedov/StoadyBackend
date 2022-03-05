namespace Stoady.Models.Handlers.Question.EditQuestion
{
    public sealed record EditQuestionRequest(
        string QuestionText,
        string AnswerText);
}
