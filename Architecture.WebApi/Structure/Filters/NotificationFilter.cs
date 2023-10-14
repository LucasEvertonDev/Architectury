using Architecture.Application.Core.Notifications.Context;
using Architecture.Application.Core.Notifications.Enum;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace Architecture.WebApi.Structure.Filters;

public class NotificationFilter : IAsyncResultFilter
{
    private readonly NotificationContext _notificationContext;

    public NotificationFilter(NotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notificationContext.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";
            
            Dictionary<object, object[]> dic = new Dictionary<object, object[]>();

            var agrupados = _notificationContext.Notifications.OrderByDescending(a => (int)a.NotificationInfo.EntityInfo.NotificationType)
                .Select(a => new
                {
                    key = a.NotificationInfo.PropInfo.MemberName ?? nameof(NotificationType.BusinessNotification),
                    message = a.Error.message,
                })
                .GroupBy(a => a.key).Select(a => new
                {
                    key = a.Key,
                    messages = a.ToList().Select(a => a.message).ToArray()
                })
                .ToList();

            agrupados.ForEach(i =>
            {
                dic.Add(i.key, i.messages);
            });
           
            var notifications = JsonConvert.SerializeObject(new
            {
                status =  400,
                errors = dic
            }); 

            await context.HttpContext.Response.WriteAsync(notifications);

            return;
        }

        await next();
    }
}