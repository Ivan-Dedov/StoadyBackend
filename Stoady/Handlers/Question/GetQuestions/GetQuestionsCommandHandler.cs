using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Stoady.DataAccess.DataContexts;
using Stoady.Models.Handlers.Question.GetQuestions;

namespace Stoady.Handlers.Question.GetQuestions
{
    public sealed class GetQuestionsCommandHandler
    : IRequestHandler<GetQuestionsCommand, GetQuestionsResponse>
    {
        private readonly StoadyDataContext _context;

        public GetQuestionsCommandHandler(
            StoadyDataContext context)
        {
            _context = context;
        }

        public async Task<GetQuestionsResponse> Handle(
            GetQuestionsCommand request,
            CancellationToken cancellationToken)
        {
            var topicId = request.TopicId;

            if (_context.Topics.Count(x => x.Id == topicId) != 1)
            {
                var message = $"Could not find topic with ID = {topicId}";
                throw new ApplicationException(message);
            }

            var questions = _context.Questions
                .Where(q => q.TopicId == topicId)
                .Select(q => new QuestionInTopic
                {
                    Id = q.Id,
                    AnswerText = q.AnswerText,
                    QuestionText = q.QuestionText
                })
                .ToList();

            return new GetQuestionsResponse
            {
                Questions = questions
            };
        }
    }

    public sealed record GetQuestionsCommand(
            long TopicId)
        : IRequest<GetQuestionsResponse>;
}
