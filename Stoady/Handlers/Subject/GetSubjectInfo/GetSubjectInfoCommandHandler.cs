using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.Subject.GetSubjectInfo;

namespace Stoady.Handlers.Subject.GetSubjectInfo
{
    public sealed record GetSubjectInfoCommand(
            long SubjectId)
        : IRequest<GetSubjectInfoResponse>;

    public sealed class GetSubjectInfoCommandHandler
        : IRequestHandler<GetSubjectInfoCommand, GetSubjectInfoResponse>
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly ILogger<GetSubjectInfoCommandHandler> _logger;

        public GetSubjectInfoCommandHandler(
            ILogger<GetSubjectInfoCommandHandler> logger,
            ISubjectRepository subjectRepository,
            ITopicRepository topicRepository)
        {
            _logger = logger;
            _subjectRepository = subjectRepository;
            _topicRepository = topicRepository;
        }

        public async Task<GetSubjectInfoResponse> Handle(
            GetSubjectInfoCommand request,
            CancellationToken ct)
        {
            var subjectId = request.SubjectId;

            var subjectTask = _subjectRepository.GetSubjectById(subjectId, ct);
            var topicsTask = _topicRepository.GetTopicsBySubjectId(subjectId, ct);

            await Task.WhenAll(subjectTask, topicsTask);

            var subject = subjectTask.Result;
            var topics = topicsTask.Result;

            if (subject is null || topics is null)
            {
                var message = $"Could not find subject with ID = {subjectId}";
                _logger.LogError(message);
                throw new ApplicationException(message);
            }

            return new GetSubjectInfoResponse
            {
                Name = subject.Title,
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
}
