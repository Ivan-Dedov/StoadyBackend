using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;

namespace Stoady.Handlers.Subject.RemoveSubject
{
    public sealed class RemoveSubjectCommandHandler
    : IRequestHandler<RemoveSubjectCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<RemoveSubjectCommandHandler> _logger;

        public RemoveSubjectCommandHandler(
            StoadyDataContext context,
            ILogger<RemoveSubjectCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            RemoveSubjectCommand request,
            CancellationToken cancellationToken)
        {
            var subjectId = request.SubjectId;

            var subjects = _context.Questions
                .Where(x => x.Id == subjectId);

            if (subjects.Count() != 1)
            {
                var message = $"Could not find subject (ID = {subjectId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var subject = subjects.First();
            _context.Questions.Remove(subject);

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not delete subject (ID = {subjectId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record RemoveSubjectCommand(
            long SubjectId)
        : IRequest<Unit>;
}
