using FluentValidation;

namespace PostaTypes.Contracts.Validators.Custom;

public static class DateTimeValidators
{
    public static IRuleBuilderOptions<T, DateTime> AfterWorkDayStarted<T>(
        this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .Must((objectRoot, dateTime, context) =>
            {
                var sunrise = TimeOnly.MinValue.AddHours(6);
                var providedTime = TimeOnly.FromDateTime(dateTime);
                context.MessageFormatter.AppendArgument("Sunrise", sunrise);
                context.MessageFormatter.AppendArgument("InputDateTime", dateTime);
                return providedTime > sunrise;
            })
            .WithMessage("{PropertyName} must be after {Sunrise}. You provided {InputDateTime}");
    }
}

