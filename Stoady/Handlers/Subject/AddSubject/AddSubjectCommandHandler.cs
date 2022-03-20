using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Models.Parameters;
using Stoady.DataAccess.Repositories.Interfaces;

namespace Stoady.Handlers.Subject.AddSubject
{
    public sealed record AddSubjectCommand(
            long TeamId,
            string SubjectName,
            string SubjectDescription)
        : IRequest<Unit>;

    public sealed class AddSubjectCommandHandler
        : IRequestHandler<AddSubjectCommand, Unit>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<AddSubjectCommandHandler> _logger;

        public AddSubjectCommandHandler(
            ILogger<AddSubjectCommandHandler> logger,
            ISubjectRepository subjectRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
        }

        public async Task<Unit> Handle(
            AddSubjectCommand request,
            CancellationToken ct)
        {
            var (teamId, subjectName, subjectDescription) = request;

            await _subjectRepository.AddSubject(
                new AddSubjectParameters
                {
                    TeamId = teamId,
                    SubjectName = subjectName,
                    SubjectDescription = subjectDescription,
                },
                ct);

            return Unit.Value;
        }
    }
}
