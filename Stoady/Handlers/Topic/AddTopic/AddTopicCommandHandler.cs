using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Stoady.DataAccess.DataContexts;
using Stoady.DataAccess.Models.Dao;

namespace Stoady.Handlers.Topic.AddTopic
{
    public sealed class AddTopicCommandHandler
        : IRequestHandler<AddTopicCommand, Unit>
    {
        private readonly StoadyDataContext _context;
        private readonly ILogger<AddTopicCommandHandler> _logger;

        public AddTopicCommandHandler(
            StoadyDataContext context,
            ILogger<AddTopicCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Unit> Handle(
            AddTopicCommand request,
            CancellationToken cancellationToken)
        {
            var (subjectId, topicName, topicDescription) = request;

            if (_context.Subjects.Count(x => x.Id == subjectId) != 1)
            {
                var message = $"Could not find subject with ID = {subjectId}";
                throw new ApplicationException(message);
            }

            await _context.Topics.AddAsync(
                new TopicDao
                {
                    SubjectId= subjectId,
                    Title = topicName,
                    Description = topicDescription
                },
                cancellationToken);

            if (await _context.SaveChangesAsync() != 1)
            {
                var message = $"Could not add topic to subject with ID = {subjectId}";
                _logger.LogWarning(message);
                throw new ApplicationException(message);
            }

            return Unit.Value;
        }
    }

    public sealed record AddTopicCommand(
            long SubjectId,
            string TopicName,
            string TopicDescription)
        : IRequest<Unit>;
}
