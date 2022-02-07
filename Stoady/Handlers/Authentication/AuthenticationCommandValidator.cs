using FluentValidation;

namespace Stoady.Handlers.Authentication
{
    public class AuthenticationCommandValidator : AbstractValidator<AuthenticationCommand>
    {
        public AuthenticationCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid e-mail provided.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty, null or whitespace-only.");
        }
    }
}
