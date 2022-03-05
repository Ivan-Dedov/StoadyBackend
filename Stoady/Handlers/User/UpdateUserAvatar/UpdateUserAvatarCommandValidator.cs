using FluentValidation;

namespace Stoady.Handlers.User.UpdateUserAvatar
{
    public class UpdateUserAvatarCommandValidator : AbstractValidator<UpdateUserAvatarCommand>
    {
        private const int MinAvatarIndex = 0;
        private const int MaxAvatarIndex = 15;

        public UpdateUserAvatarCommandValidator()
        {
            RuleFor(x => x.AvatarId)
                .GreaterThanOrEqualTo(MinAvatarIndex)
                .LessThanOrEqualTo(MaxAvatarIndex);
        }
    }
}
