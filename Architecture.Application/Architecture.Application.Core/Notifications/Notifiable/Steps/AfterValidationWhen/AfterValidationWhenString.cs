using Architecture.Application.Core.Notifications.Notifiable.Steps.AddNotification;
using System.Text.RegularExpressions;

namespace Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;

public class AfterValidationWhenString : AfterValidationWhen, IAfterValidationWhen
{
    protected string _currentvalue { get; set; }

    public AfterValidationWhenString(NotificationContext notificationContext, NotificationInfo notificationInfo) : base(notificationContext, notificationInfo)
    {
        _currentvalue = notificationInfo.Value;
    }

    public AddNotificationService<AfterValidationWhenString> Is(bool ruleFor)
    {
        return new AddNotificationService<AfterValidationWhenString>(_notificationContext, ruleFor, _notificationInfo);
    }

    public AddNotificationService<AfterValidationWhenString> IsNull(bool ruleFor)
    {
        return new AddNotificationService<AfterValidationWhenString>(_notificationContext, ruleFor, _notificationInfo);
    }

    public AddNotificationService<AfterValidationWhenString> IsInvalidEmail()
    {
        return new AddNotificationService<AfterValidationWhenString>(_notificationContext, !Regex.IsMatch(_currentvalue, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"), _notificationInfo);
    }

    public AddNotificationService<AfterValidationWhenString> IsNullOrEmpty()
    {
        return new AddNotificationService<AfterValidationWhenString>(_notificationContext, string.IsNullOrEmpty(_currentvalue), _notificationInfo);
    }

    public AddNotificationService<AfterValidationWhenString> MinLength(int minLenght)
    {
        return new AddNotificationService<AfterValidationWhenString>(_notificationContext, _currentvalue?.Length < minLenght, _notificationInfo);
    }

    public AddNotificationService<AfterValidationWhenString> MaxLenght(int maxLenght)
    {
        return new AddNotificationService<AfterValidationWhenString>(_notificationContext, _currentvalue?.Length > maxLenght, _notificationInfo);
    }
}
