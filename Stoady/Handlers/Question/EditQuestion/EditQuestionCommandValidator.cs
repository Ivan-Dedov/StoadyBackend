using FluentValidation;

namespace Stoady.Handlers.Question.EditQuestion
{
    public class EditQuestionCommandValidator : AbstractValidator<EditQuestionCommand>
    {
        private const int MinQuestionLength = 1;
        private const int MaxQuestionLength = 100;

        private const int MinAnswerLength = 1;
        private const int MaxAnswerLength = 100;

        public EditQuestionCommandValidator()
        {
            RuleFor(x => x.QuestionText)
                .Length(MinQuestionLength, MaxQuestionLength);

            RuleFor(x => x.AnswerText)
                .Length(MinAnswerLength, MaxAnswerLength);
        }
    }
}
