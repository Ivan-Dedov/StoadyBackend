using FluentValidation;

namespace Stoady.Handlers.Team.CreateTeam
{
    public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
    {
        public CreateTeamCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.TeamName)
                .NotEmpty();
        }
    }
}
