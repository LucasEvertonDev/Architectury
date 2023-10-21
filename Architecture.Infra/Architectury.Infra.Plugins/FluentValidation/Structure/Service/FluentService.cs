using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Plugins.FluentValidation;
using FluentValidation;
using FluentValidation.Results;

namespace Architectury.Infra.Plugins.FluentValidation.Structure.Service;

public class FluentService : IFluentService
{
    private readonly IServiceProvider _serviceProvider;

    public FluentService(IServiceProvider  serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<IEnumerable<NotificationModel>> ValidateParameterAsync(object parameter)
    {
        var typeParam = parameter.GetType();
        if (typeParam.GetInterface(nameof(IValidationAsync)) != null)
        {
            var failures = await GetValidationErrorsAsync(parameter);

            if (failures.Any())
            {
                return GetNotifications(failures, typeParam);
            }
        }

        return new List<NotificationModel>();
    }

    private IEnumerable<NotificationModel> GetNotifications(IEnumerable<ValidationFailure> failures, Type typeParam)
    {
        foreach (var failure in failures)
        {
            yield return new NotificationModel(
                failure: new FailureModel(
                    failure.ErrorCode, failure.ErrorMessage
                ),
                notificationInfo: new NotificationInfo(
                    new PropInfo()
                    {
                        MemberName = failure.PropertyName,
                    },
                    entityInfo: new EntityInfo
                    {
                        Name = typeParam.Name,
                        Namespace = typeParam.Namespace,
                        NotificationType = Architecture.Application.Core.Notifications.Enum.NotificationType.RequestValidation
                    }
                )
            );
        }
    }

    private async Task<IEnumerable<ValidationFailure>> GetValidationErrorsAsync(object value)
    {
        if (value == null)
        {
            return new[] { new ValidationFailure("", "instance is null") };
        }

        var genericValidatorType = typeof(IValidator<>);
        var specificValidatorType = genericValidatorType.MakeGenericType(value.GetType());

        var validatorInstance = (IValidator)_serviceProvider.GetService(specificValidatorType);

        if (validatorInstance == null)
        {
            return new List<ValidationFailure>();
        }

        var validationResult = await validatorInstance.ValidateAsync(new ValidationContext<object>(value));
        return validationResult.Errors ?? new List<ValidationFailure>();
    }
}
