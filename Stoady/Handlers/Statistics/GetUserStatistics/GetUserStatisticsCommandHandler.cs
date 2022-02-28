using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.Models.Handlers.Statistics.GetUserStatistics;

namespace Stoady.Handlers.Statistics.GetUserStatistics
{
    public sealed class GetUserStatisticsCommandHandler
        : IRequestHandler<GetUserStatisticsCommand, GetUserStatisticsResponse>
    {
        public Task<GetUserStatisticsResponse> Handle(
            GetUserStatisticsCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record GetUserStatisticsCommand(
            long UserId)
        : IRequest<GetUserStatisticsResponse>;
}
