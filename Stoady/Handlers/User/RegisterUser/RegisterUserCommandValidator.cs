using FluentValidation;

namespace Stoady.Handlers.User.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private const int MinPasswordLength = 5;
        private const int MaxPasswordLength = 30;

        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Username)
                .NotEmpty();

            RuleFor(x => x.Password.Length)
                .GreaterThanOrEqualTo(MinPasswordLength)
                .LessThanOrEqualTo(MaxPasswordLength);
        }
    }
}
