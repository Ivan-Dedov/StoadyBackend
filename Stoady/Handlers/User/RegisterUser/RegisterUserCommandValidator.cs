using FluentValidation;

namespace Stoady.Handlers.User.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private const int MinPasswordLength = 5;
        private const int MaxPasswordLength = 30;

        private const int MinAvatarIndex = 0;
        private const int MaxAvatarIndex = 14;

        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Username)
                .NotEmpty();

            RuleFor(x => x.Password.Length)
                .GreaterThanOrEqualTo(MinPasswordLength)
                .LessThanOrEqualTo(MaxPasswordLength);

            RuleFor(x => x.AvatarId)
                .GreaterThanOrEqualTo(MinAvatarIndex)
                .LessThanOrEqualTo(MaxAvatarIndex);
        }
    }
}
