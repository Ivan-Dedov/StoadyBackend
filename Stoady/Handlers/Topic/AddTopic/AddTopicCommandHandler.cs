using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Topic.AddTopic
{
    public sealed class AddTopicCommandHandler
        : IRequestHandler<AddTopicCommand, Unit>
    {
        public Task<Unit> Handle(
            AddTopicCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record AddTopicCommand(
            long TopicId,
            string QuestionText,
            string AnswerText)
        : IRequest<Unit>;
}
