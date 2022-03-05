using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.DataAccess.Models.Dao;

namespace Stoady.Handlers.Subject.AddSubject
{
    public sealed class AddSubjectCommandHandler
        : IRequestHandler<AddSubjectCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<AddSubjectCommandHandler> _logger;

        public AddSubjectCommandHandler(
            StoadyDataContext context,
            ILogger<AddSubjectCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            AddSubjectCommand request,
            CancellationToken cancellationToken)
        {
            var (teamId, subjectName, subjectPicture, subjectDescription) = request;

            if (_context.Teams.Count(x => x.Id == teamId) != 1)
            {
                var message = $"Could not find team with ID = {teamId}";
                throw new ApplicationException(message);
            }

            await _context.Subjects.AddAsync(
                new SubjectDao
                {
                    TeamId = teamId,
                    Title = subjectName,
                    Image = subjectPicture,
                    Description = subjectDescription
                },
                cancellationToken);

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not add subject to team with ID = {teamId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record AddSubjectCommand(
            long TeamId,
            string SubjectName,
            string SubjectPicture,
            string SubjectDescription)
        : IRequest<Unit>;
}
