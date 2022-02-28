using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Question.AddQuestion
{
    public sealed class AddQuestionCommandHandler
        : IRequestHandler<AddQuestionCommand, Unit>
    {
        public Task<Unit> Handle(
            AddQuestionCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record AddQuestionCommand(
            long TopicId,
            string QuestionText,
            string AnswerText)
        : IRequest<Unit>;
}
