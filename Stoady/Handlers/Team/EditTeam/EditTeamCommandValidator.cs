using FluentValidation;

namespace Stoady.Handlers.Team.EditTeam
{
    public class EditTeamCommandValidator : AbstractValidator<EditTeamCommand>
    {
        public EditTeamCommandValidator()
        {
            RuleFor(x => x.ExecutorId)
                .GreaterThan(0);

            RuleFor(x => x.TeamAvatar)
                .NotEmpty();

            RuleFor(x => x.TeamName)
                .NotEmpty();
        }
    }
}
