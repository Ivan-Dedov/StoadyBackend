using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.Models.Handlers.Team.GetTeamMembers;

namespace Stoady.Handlers.Team.GetTeamMembers
{
    public sealed class GetTeamMembersCommandHandler
        : IRequestHandler<GetTeamMembersCommand, GetTeamMembersResponse>
    {
        public Task<GetTeamMembersResponse> Handle(
            GetTeamMembersCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record GetTeamMembersCommand(
            long TeamId)
        : IRequest<GetTeamMembersResponse>;
}
