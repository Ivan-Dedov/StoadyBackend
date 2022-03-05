using FluentValidation;

namespace Stoady.Handlers.Topic.EditTopic
{
    public class EditTopicCommandValidator : AbstractValidator<EditTopicCommand>
    {
        public EditTopicCommandValidator()
        {
            RuleFor(x => x.TopicDescription)
                .NotEmpty();

            RuleFor(x => x.TopicName)
                .NotEmpty();
        }
    }
}
