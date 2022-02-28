using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Subject.EditSubject
{
    public sealed class EditSubjectCommandHandler
    : IRequestHandler<EditSubjectCommand, Unit>
    {
        public Task<Unit> Handle(
            EditSubjectCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record EditSubjectCommand(
            long SubjectId,
            string SubjectName,
            string SubjectPicture,
            string SubjectDescription)
        : IRequest<Unit>;
}
