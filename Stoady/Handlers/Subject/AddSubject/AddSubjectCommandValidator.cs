using FluentValidation;

namespace Stoady.Handlers.Subject.AddSubject
{
    public class AddSubjectCommandValidator : AbstractValidator<AddSubjectCommand>
    {
        private const int MinNameLength = 1;
        private const int MaxNameLength = 50;

        private const int MinDescriptionLength = 0;
        private const int MaxDescriptionLength = 250;

        public AddSubjectCommandValidator()
        {
            RuleFor(x => x.TeamId)
                .GreaterThan(0);

            RuleFor(x => x.SubjectName)
                .Length(MinNameLength, MaxNameLength);

            RuleFor(x => x.SubjectDescription)
                .Length(MinDescriptionLength, MaxDescriptionLength);
        }
    }
}
