using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Team.CreateTeam
{
    public sealed class CreateTeamCommandHandler
        : IRequestHandler<CreateTeamCommand, Unit>
    {
        public Task<Unit> Handle(
            CreateTeamCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record CreateTeamCommand(
            long UserId,
            string TeamName)
        : IRequest<Unit>;
}
