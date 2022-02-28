using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.Models.Handlers.Team.GetUserTeams;

namespace Stoady.Handlers.Team.GetUserTeams
{
    public sealed class GetUserTeamsCommandHandler
        : IRequestHandler<GetUserTeamsCommand, GetUserTeamsResponse>
    {
        public Task<GetUserTeamsResponse> Handle(
            GetUserTeamsCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record GetUserTeamsCommand(
            long UserId)
        : IRequest<GetUserTeamsResponse>;
}
