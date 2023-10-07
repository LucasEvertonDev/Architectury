using Architecture.Application.Core.Notifications.Services;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;

public interface IRecordNotifiable : INotifiable
{
    bool IsValid();
}