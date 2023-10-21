namespace Architecture.Application.Domain.Plugins.FluentValidation;

public interface IFluentService
{
    Task<IEnumerable<NotificationModel>> ValidateParameterAsync(object parameter);
}
