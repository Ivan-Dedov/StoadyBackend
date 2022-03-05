using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;

namespace Stoady.Handlers.Subject.EditSubject
{
    public sealed class EditSubjectCommandHandler
    : IRequestHandler<EditSubjectCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<EditSubjectCommandHandler> _logger;

        public EditSubjectCommandHandler(
            StoadyDataContext context,
            ILogger<EditSubjectCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            EditSubjectCommand request,
            CancellationToken cancellationToken)
        {
            var (subjectId, subjectName, subjectPicture, subjectDescription) = request;

            var subjects = _context.Subjects
                .Where(x => x.Id == subjectId);

            if (subjects.Count() != 1)
            {
                var message = $"Could not find subject (ID = {subjectId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var subject = subjects.First();
            subject.Title = subjectName;
            subject.Image = subjectPicture;
            subject.Description = subjectDescription;

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not edit subject (ID = {subjectId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record EditSubjectCommand(
            long SubjectId,
            string SubjectName,
            string SubjectPicture,
            string SubjectDescription)
        : IRequest<Unit>;
}
