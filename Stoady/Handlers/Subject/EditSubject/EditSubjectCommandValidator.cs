using FluentValidation;

namespace Stoady.Handlers.Subject.EditSubject
{
    public class EditSubjectCommandValidator : AbstractValidator<EditSubjectCommand>
    {
        private const int MinNameLength = 1;
        private const int MaxNameLength = 50;

        private const int MinDescriptionLength = 0;
        private const int MaxDescriptionLength = 250;

        public EditSubjectCommandValidator()
        {
            RuleFor(x => x.SubjectName)
                .Length(MinNameLength, MaxNameLength);

            RuleFor(x => x.SubjectDescription)
                .Length(MinDescriptionLength, MaxDescriptionLength);
        }
    }
}
