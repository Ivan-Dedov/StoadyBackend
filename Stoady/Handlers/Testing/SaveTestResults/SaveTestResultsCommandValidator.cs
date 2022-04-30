using FluentValidation;

namespace Stoady.Handlers.Testing.SaveTestResults
{
    public class SaveTestResultsCommandValidator : AbstractValidator<SaveTestResultsCommand>
    {
        private const int MinResult = 0;
        private const int MaxResult = 100;

        public SaveTestResultsCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.TopicId)
                .GreaterThan(0);

            RuleFor(x => x.Result)
                .GreaterThanOrEqualTo(MinResult)
                .LessThanOrEqualTo(MaxResult);
        }
    }
}
