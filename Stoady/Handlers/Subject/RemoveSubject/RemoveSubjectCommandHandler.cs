using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Subject.RemoveSubject
{
    public sealed class RemoveSubjectCommandHandler
    : IRequestHandler<RemoveSubjectCommand, Unit>
    {
        public Task<Unit> Handle(
            RemoveSubjectCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record RemoveSubjectCommand(
            long SubjectId)
        : IRequest<Unit>;
}
