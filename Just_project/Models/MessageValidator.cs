using FluentValidation;

namespace Just_project.Models
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(r => r.message).NotEmpty();

        }
    }

}