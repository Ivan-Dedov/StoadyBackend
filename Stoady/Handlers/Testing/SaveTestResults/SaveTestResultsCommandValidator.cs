using FluentValidation;

namespace Stoady.Handlers.Testing.SaveTestResults
{
    public class SaveTestResultsCommandValidator : AbstractValidator<SaveTestResultsCommand>
    {
        public SaveTestResultsCommandValidator()
        {
            RuleFor(x => x.Result)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(100);
        }
    }
}
