using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.DataAccess.Repositories.Interfaces;
using Stoady.Models.Handlers.Question.GetQuestions;

namespace Stoady.Handlers.Question.GetQuestions
{
    public sealed record GetQuestionsCommand(
            long TopicId)
        : IRequest<GetQuestionsResponse>;

    public sealed class GetQuestionsCommandHandler
        : IRequestHandler<GetQuestionsCommand, GetQuestionsResponse>
    {
        private readonly IQuestionRepository _questionRepository;

        public GetQuestionsCommandHandler(
            IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<GetQuestionsResponse> Handle(
            GetQuestionsCommand request,
            CancellationToken ct)
        {
            var topicId = request.TopicId;

            var questions = (await _questionRepository
                    .GetQuestionsByTopicId(topicId, ct))
                .Select(q => new QuestionInTopic
                {
                    Id = q.Id,
                    QuestionText = q.QuestionText,
                    AnswerText = q.AnswerText
                })
                .ToList();

            return new GetQuestionsResponse
            {
                Questions = questions
            };
        }
    }
}
