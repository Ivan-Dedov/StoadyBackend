using FluentValidation;

namespace Stoady.Handlers.Topic.AddTopic
{
    public class AddTopicCommandValidator : AbstractValidator<AddTopicCommand>
    {
        private const int MinNameLength = 1;
        private const int MaxNameLength = 50;

        private const int MinDescriptionLength = 0;
        private const int MaxDescriptionLength = 250;

        public AddTopicCommandValidator()
        {
            RuleFor(x => x.SubjectId)
                .GreaterThan(0);

            RuleFor(x => x.TopicDescription)
                .Length(MinDescriptionLength, MaxDescriptionLength);

            RuleFor(x => x.TopicName)
                .Length(MinNameLength, MaxNameLength);
        }
    }
}
