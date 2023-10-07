using System.Text.RegularExpressions;

namespace Architecture.Application.Core.Notifications.Services
{
    public partial class ConditionalNotificationsService 
    {
        private readonly NotificationContext _notificationContext;

        public ConditionalNotificationsService(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        public NotificationService Is(bool ruleFor)
        {
            return new NotificationService(_notificationContext, ruleFor);
        }

        public NotificationService IsInvalidEmail(string email)
        {
            return new NotificationService(_notificationContext, !Regex.IsMatch((string)email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"));
        }

        public NotificationService IsNullOrEmpty(string value)
        {
            return new NotificationService(_notificationContext, string.IsNullOrEmpty(value));
        }
    }

    public class NotificationService
    {
        private readonly NotificationContext _notificationContext;
        private readonly bool _includeNotification;

        public NotificationService(NotificationContext notificationContext, bool includeNotification)
        {
            _notificationContext = notificationContext;
            _includeNotification = includeNotification;
        }

        public ConditionalNotificationsService Notification(NotificationModel notification)
        {
            if (_includeNotification)
            {
                _notificationContext.AddNotification(notification);
            }

            return new ConditionalNotificationsService(_notificationContext);
        }
    }
}
