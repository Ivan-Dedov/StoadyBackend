using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Stoady.Handlers.Question.EditQuestion
{
    public sealed class EditQuestionCommandHandler
    : IRequestHandler<EditQuestionCommand, Unit>
    {
        public Task<Unit> Handle(
            EditQuestionCommand request,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }

    public sealed record EditQuestionCommand(
            long QuestionId,
            string QuestionText,
            string AnswerText)
        : IRequest<Unit>;
}
