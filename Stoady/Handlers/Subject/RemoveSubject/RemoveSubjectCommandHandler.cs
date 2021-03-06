using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Subject.RemoveSubject
{
    public sealed record RemoveSubjectCommand(
            long SubjectId)
        : IRequest<Unit>;

    public sealed class RemoveSubjectCommandHandler
    : IRequestHandler<RemoveSubjectCommand, Unit>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<RemoveSubjectCommandHandler> _logger;

        public RemoveSubjectCommandHandler(
            ILogger<RemoveSubjectCommandHandler> logger,
            ISubjectRepository subjectRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }

        public async Task<Unit> Handle(
            RemoveSubjectCommand request,
            CancellationToken ct)
        {
            var subjectId = request.SubjectId;

            var result = await _subjectRepository.RemoveSubject(subjectId, ct);

            if (result != 1)
            {
                const string message = "Something went wrong when removing the subject. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
