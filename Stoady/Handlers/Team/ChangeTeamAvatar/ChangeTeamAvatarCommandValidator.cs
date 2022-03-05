using FluentValidation;

namespace Stoady.Handlers.Team.ChangeTeamAvatar
{
    public class ChangeTeamAvatarCommandValidator : AbstractValidator<ChangeTeamAvatarCommand>
    {
        public ChangeTeamAvatarCommandValidator()
        {
            RuleFor(x => x.ExecutorId)
                .GreaterThan(0);

            RuleFor(x => x.TeamAvatar)
                .NotEmpty();
        }
    }
}
