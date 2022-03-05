using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.Models.Handlers.Subject.GetSubjectInfo;

namespace Stoady.Handlers.Subject.GetSubjectInfo
{
    public sealed class GetSubjectInfoCommandHandler
        : IRequestHandler<GetSubjectInfoCommand, GetSubjectInfoResponse>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<GetSubjectInfoCommandHandler> _logger;

        public GetSubjectInfoCommandHandler(
            StoadyDataContext context,
            ILogger<GetSubjectInfoCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GetSubjectInfoResponse> Handle(
            GetSubjectInfoCommand request,
            CancellationToken cancellationToken)
        {
            var subjectId = request.SubjectId;

            var subjects = _context.Subjects
                .Where(x => x.Id == subjectId);

            if (subjects.Count() != 1)
            {
                var message = $"Could not find subject (ID = {subjectId})";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            var subject = subjects.First();

            var topics = _context.Topics
                .Where(x => x.SubjectId == subjectId);

            return new GetSubjectInfoResponse
            {
                Name = subject.Title,
                Picture = subject.Image,
                Description = subject.Description,
                Topics = topics.Select(x =>
                        new TopicInSubject
                        {
                            Id = x.Id,
                            Name = x.Title,
                        })
                    .ToList()
            };
        }
    }

    public sealed record GetSubjectInfoCommand(
            long SubjectId)
        : IRequest<GetSubjectInfoResponse>;
}
