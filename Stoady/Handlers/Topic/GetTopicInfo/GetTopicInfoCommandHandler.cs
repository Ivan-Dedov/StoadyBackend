using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.Models.Handlers.Team.GetTeamInfo;

namespace Stoady.Handlers.Topic.GetTopicInfo
{
    public sealed class GetTopicInfoCommandHandler
        : IRequestHandler<GetTopicInfoCommand, GetTeamInfoResponse>
    {
        public Task<GetTeamInfoResponse> Handle(
            GetTopicInfoCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record GetTopicInfoCommand(
            long TopicInfo)
        : IRequest<GetTeamInfoResponse>;
}
