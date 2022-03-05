using FluentValidation;

namespace Stoady.Handlers.User.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private const int HashedPasswordLength = 64;

        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Username)
                .NotEmpty();

            RuleFor(x => x.Password)
                .Length(HashedPasswordLength);
        }
    }
}
