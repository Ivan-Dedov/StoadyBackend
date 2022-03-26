using FluentValidation;

namespace Stoady.Handlers.User.GetUserTeams
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
