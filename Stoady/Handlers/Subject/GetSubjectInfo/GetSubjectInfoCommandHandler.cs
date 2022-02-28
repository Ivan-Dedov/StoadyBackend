using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.Models.Handlers.Subject.GetSubjectInfo;

namespace Stoady.Handlers.Subject.GetSubjectInfo
{
    public sealed class GetSubjectInfoCommandHandler
    : IRequestHandler<GetSubjectInfoCommand, GetSubjectInfoResponse>
    {
        public Task<GetSubjectInfoResponse> Handle(
            GetSubjectInfoCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record GetSubjectInfoCommand(
            long SubjectId)
        : IRequest<GetSubjectInfoResponse>;
}
