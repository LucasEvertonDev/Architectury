using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;

namespace Architecture.Application.Core.Structure.Extensions;

public static class List 
{
    public static bool HasFailures<T>(this List<T> list) where T : INotifiableModel
    {
        return list.Exists(item => item.GetFailures().Any());
    }

    public static List<NotificationModel> GetNotifications<T>(this List<T> list, string prefix) where T : INotifiableModel
    {
        var notifications = new List<NotificationModel>();

        for (var i = 0; i < list.Count; i++)
        {
            var item = list[i];
            if (item.GetFailures().Any())
            {
                notifications.AddRange(
                    item.GetFailures().Select(notication =>
                    {
                        var nomeRedundanteDoObjetoDaLista = notication.member.IndexOf('.');
                        notication.SetMember($"{prefix}[{i}].{notication.member.Substring(nomeRedundanteDoObjetoDaLista + 1)}");

                        return notication;
                    })
                    .ToList()
                );
            }
        }

        return notifications;
    }
}
