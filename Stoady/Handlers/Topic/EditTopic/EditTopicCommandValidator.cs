using FluentValidation;

namespace Stoady.Handlers.Topic.EditTopic
{
    public class EditTopicCommandValidator : AbstractValidator<EditTopicCommand>
    {
        private const int MinNameLength = 1;
        private const int MaxNameLength = 50;

        private const int MinDescriptionLength = 0;
        private const int MaxDescriptionLength = 250;

        public EditTopicCommandValidator()
        {
            RuleFor(x => x.TopicDescription)
                .Length(MinDescriptionLength, MaxDescriptionLength);

            RuleFor(x => x.TopicName)
                .Length(MinNameLength, MaxNameLength);
        }
    }
}
