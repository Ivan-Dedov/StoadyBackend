using FluentValidation;

namespace Stoady.Handlers.Topic.AddTopic
{
    public class AddTopicCommandValidator : AbstractValidator<AddTopicCommand>
    {
        public AddTopicCommandValidator()
        {
            RuleFor(x => x.SubjectId)
                .GreaterThan(0);

            RuleFor(x => x.TopicDescription)
                .NotEmpty();

            RuleFor(x => x.TopicName)
                .NotEmpty();
        }
    }
}
