using FluentValidation;
using PostaTypes.Contracts.Requests.Validation;
using PostaTypes.Contracts.Validators.Custom;

namespace PostaTypes.Contracts.Validators
{
    public class ValidationCheckRequestValidator : AbstractValidator<ValidationCheckRequest>
    {
        public ValidationCheckRequestValidator()
        {
            RuleFor(x => x.Name).Length(3, 10);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.DateSend).Must(x => x > DateTime.UtcNow);
            RuleFor(x => x.DateWork).AfterWorkDayStarted(); // awr: custom validator 
            RuleForEach(x => x.Reciepients).Must(x => x.Length > 0)
                       .WithMessage("Пустые адресаты не допускаются");

        }
    }
}
