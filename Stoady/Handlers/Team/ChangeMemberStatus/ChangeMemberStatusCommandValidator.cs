using FluentValidation;

namespace Stoady.Handlers.Team.ChangeMemberStatus
{
    public class ChangeMemberStatusCommandValidator : AbstractValidator<ChangeMemberStatusCommand>
    {
        public ChangeMemberStatusCommandValidator()
        {
            RuleFor(x => x.ExecutorId)
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}
