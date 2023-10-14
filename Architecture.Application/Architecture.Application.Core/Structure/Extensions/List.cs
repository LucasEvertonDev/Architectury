using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        if (list == null || list.Count() == 0)
        {
            return notifications;
        }

        for (var i = 0; i < list.Count; i++)
        {
            var item = list[i];
            if (item.GetFailures().Any())
            {
                notifications.AddRange(
                    item.GetFailures().Select(notf =>
                    {
                        var notification = notf.Clone();

                        var nomeRedundanteDoObjetoDaLista = notf.NotificationInfo.PropInfo.MemberName.IndexOf('.');
                        notification.NotificationInfo.PropInfo.MemberName = $"{prefix}[{i}].{notf.NotificationInfo.PropInfo.MemberName.Substring(nomeRedundanteDoObjetoDaLista + 1)}";

                        return notification;
                    })
                    .ToList()
                );
            }
        }

        return notifications;
    }
}

public static class ExtensionMethods
{
    public static T Clone<T>(this T a)
    {
        string jsonString = JsonConvert.SerializeObject(a);
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
}
