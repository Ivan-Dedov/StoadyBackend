using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.DataAccess.DataContexts;
using Stoady.Models.Handlers.Statistics.GetUserStatistics;

namespace Stoady.Handlers.Statistics.GetUserStatistics
{
    public sealed class GetUserStatisticsCommandHandler
        : IRequestHandler<GetUserStatisticsCommand, GetUserStatisticsResponse>
    {
        private readonly StoadyDataContext _context;

        public GetUserStatisticsCommandHandler(
            StoadyDataContext context)
        {
            _context = context;
        }

        public async Task<GetUserStatisticsResponse> Handle(
            GetUserStatisticsCommand request,
            CancellationToken cancellationToken)
        {
            var userId = request.UserId;

            if (_context.Users.Count(x => x.Id == userId) != 1)
            {
                var message = $"Could not find user with ID = {userId}";
                throw new ApplicationException(message);
            }

            var results = _context.Statistics
                .Where(x => x.UserId == userId)
                .Select(x =>
                    new TopicStatistics
                    {
                        TopicId = x.TopicId,
                        Result = x.Result
                    })
                .ToList();

            return new GetUserStatisticsResponse
            {
                Results = results
            };
        }
    }

    public sealed record GetUserStatisticsCommand(
            long UserId)
        : IRequest<GetUserStatisticsResponse>;
}
