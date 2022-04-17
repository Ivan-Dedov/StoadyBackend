using System;
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
            var (subjectId, subjectName, subjectDescription) = request;

            var result = await _subjectRepository.EditSubject(
                new EditSubjectParameters
                {
                    SubjectId = subjectId,
                    SubjectName = subjectName,
                    SubjectDescription = subjectDescription
                },
                ct);

            if (result != 1)
            {
                const string message = "Something went wrong when editing the subject. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
