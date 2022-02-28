using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Subject.AddSubject
{
    public sealed class AddSubjectCommandHandler
        : IRequestHandler<AddSubjectCommand, Unit>
    {
        public Task<Unit> Handle(
            AddSubjectCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record AddSubjectCommand(
            long TeamId,
            string SubjectName,
            string SubjectPicture,
            string SubjectDescription)
        : IRequest<Unit>;
}
