using FluentValidation;

using Stoady.Models;

namespace Stoady.Handlers.Team.ChangeMemberStatus
{
    public class ChangeMemberStatusCommandValidator : AbstractValidator<ChangeMemberStatusCommand>
    {
        public ChangeMemberStatusCommandValidator()
        {
            RuleFor(x => x.ExecutorId)
                .GreaterThan(0);

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .NotEqual(x => x.ExecutorId)
                .WithMessage("'UserId' must not be equal to 'ExecutorId'.");

            RuleFor(x => x.Role)
                .NotEmpty()
                .NotEqual(Role.Creator)
                .WithMessage("Cannot assign Creator role manually.");
        }
    }
}
