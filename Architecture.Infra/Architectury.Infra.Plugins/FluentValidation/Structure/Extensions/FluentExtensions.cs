using FluentValidation;
using FluentValidation.Validators;
using Notification.Notifications;

namespace Architectury.Infra.Plugins.FluentValidation.Structure.Extensions;

public static class FluentExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, FailureModel errorModel)
    {
        return rule.WithMessage(errorModel.message).WithErrorCode(errorModel.code);
    }

    public static IRuleBuilderOptions<T, TProperty> NotNullOrEmpty<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new NotEmptyValidator<T, TProperty>());
    }
}
