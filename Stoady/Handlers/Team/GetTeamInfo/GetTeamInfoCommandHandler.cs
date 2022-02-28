using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.Models.Handlers.Team.GetTeamInfo;

namespace Stoady.Handlers.Team.GetTeamInfo
{
    public sealed class GetTeamInfoCommandHandler
        : IRequestHandler<GetTeamInfoCommand, GetTeamInfoResponse>
    {
        public Task<GetTeamInfoResponse> Handle(
            GetTeamInfoCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record GetTeamInfoCommand(
            long TeamId)
        : IRequest<GetTeamInfoResponse>;
}
