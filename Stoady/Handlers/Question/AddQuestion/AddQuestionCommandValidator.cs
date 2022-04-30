using FluentValidation;

namespace Stoady.Handlers.Question.AddQuestion
{
    public class AddQuestionCommandValidator : AbstractValidator<AddQuestionCommand>
    {
        private const int MinQuestionLength = 1;
        private const int MaxQuestionLength = 100;

        private const int MinAnswerLength = 1;
        private const int MaxAnswerLength = 100;

        public AddQuestionCommandValidator()
        {
            RuleFor(x => x.TopicId)
                .GreaterThan(0);

            RuleFor(x => x.QuestionText)
                .Length(MinQuestionLength, MaxQuestionLength);

            RuleFor(x => x.AnswerText)
                .Length(MinAnswerLength, MaxAnswerLength);
        }
    }
}
