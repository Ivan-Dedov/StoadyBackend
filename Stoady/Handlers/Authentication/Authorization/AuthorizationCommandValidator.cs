using FluentValidation;

namespace Stoady.Handlers.Authentication.Authorization
{
    public class AuthorizationCommandValidator : AbstractValidator<AuthorizationCommand>
    {
        public AuthorizationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotNull();

            RuleFor(x => x.Password)
                .NotNull();
        }
    }
}
