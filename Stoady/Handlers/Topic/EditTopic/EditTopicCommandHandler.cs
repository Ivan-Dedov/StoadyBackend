using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Topic.EditTopic
{
    public sealed class EditTopicCommandHandler
    : IRequestHandler<EditTopicCommand, Unit>
    {
        public Task<Unit> Handle(
            EditTopicCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record EditTopicCommand(
            long QuestionId,
            string QuestionText,
            string AnswerText)
        : IRequest<Unit>;
}
