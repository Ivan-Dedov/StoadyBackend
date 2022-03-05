using FluentValidation;

namespace Stoady.Handlers.Team.GetUserTeams
{
    public class GetUserTeamsCommandValidator : AbstractValidator<GetUserTeamsCommand>
    {
        public GetUserTeamsCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
    }
}
