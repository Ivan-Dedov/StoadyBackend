using FluentValidation;

namespace Stoady.Handlers.Team.RemoveMember
{
    public class RemoveMemberCommandValidator : AbstractValidator<RemoveMemberCommand>
    {
        public RemoveMemberCommandValidator()
        {
            RuleFor(x => x.ExecutorId)
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}
