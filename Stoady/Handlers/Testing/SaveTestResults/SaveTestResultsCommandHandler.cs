using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Testing.SaveTestResults
{
    public sealed class SaveTestResultsCommandHandler
    : IRequestHandler<SaveTestResultsCommand, Unit>
    {
        public Task<Unit> Handle(
            SaveTestResultsCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record SaveTestResultsCommand(
            long UserId,
            long TopicId,
            int Result)
        : IRequest<Unit>;
}
