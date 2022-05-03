using System;
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
        private readonly ITeamRepository _teamRepository;

        public AddSubjectCommandHandler(
            ILogger<AddSubjectCommandHandler> logger,
            ISubjectRepository subjectRepository,
            ITeamRepository teamRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
            _teamRepository = teamRepository;
        }

        public async Task<Unit> Handle(
            AddSubjectCommand request,
            CancellationToken ct)
        {
            var (teamId, subjectName, subjectDescription) = request;

            if (await _teamRepository.GetTeamById(teamId, ct) is null)
            {
                var message = $"Could not find team with ID = {teamId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var result = await _subjectRepository.AddSubject(
                new AddSubjectParameters
                {
                    TeamId = teamId,
                    SubjectName = subjectName,
                    SubjectDescription = subjectDescription,
                },
                ct);

            if (result != 1)
            {
                const string message = "Something went wrong when creating the subject. Please, try again.";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }
}
