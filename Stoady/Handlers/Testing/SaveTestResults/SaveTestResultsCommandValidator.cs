using FluentValidation;

namespace Stoady.Handlers.Testing.SaveTestResults
{
    public class SaveTestResultsCommandValidator : AbstractValidator<SaveTestResultsCommand>
    {
        private const int MinResult = 0;
        private const int MaxResult = 0;
        public SaveTestResultsCommandValidator()
        {
            RuleFor(x => x.Result)
                .GreaterThanOrEqualTo(MinResult)
                .LessThanOrEqualTo(MaxResult);
        }
    }
}
