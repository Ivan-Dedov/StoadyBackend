using FluentValidation;

namespace Stoady.Handlers.User.GetUserAvatar
{
    public class GetUserAvatarCommandValidator : AbstractValidator<GetUserAvatarCommand>
    {
        public GetUserAvatarCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}
