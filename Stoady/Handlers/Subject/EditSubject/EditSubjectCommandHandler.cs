using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Subject.EditSubject
{
    public sealed record EditSubjectCommand(
            long SubjectId,
            string SubjectName,
            string SubjectPicture,
            string SubjectDescription)
        : IRequest<Unit>;

    public sealed class EditSubjectCommandHandler
        : IRequestHandler<EditSubjectCommand, Unit>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<EditSubjectCommandHandler> _logger;

        public EditSubjectCommandHandler(
            ILogger<EditSubjectCommandHandler> logger,
            ISubjectRepository subjectRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }

        public async Task<Unit> Handle(
            EditSubjectCommand request,
            CancellationToken ct)
        {
            var (subjectId, subjectName, subjectPicture, subjectDescription) = request;

            await _subjectRepository.EditSubject(
                new EditSubjectParameters
                {
                    SubjectId = subjectId,
                    SubjectName = subjectName,
                    SubjectPicture = subjectPicture,
                    SubjectDescription = subjectDescription
                },
                ct);

            return Unit.Value;
        }
    }
}
