using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Notifications.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Architecture.WebApi.Controllers;

public class BaseController : Controller
{

    [ApiExplorerSettings(IgnoreApi = true)]
    public BadRequestObjectResult BadRequestFailure(Result result)
    {
        Dictionary<object, object[]> dic = new Dictionary<object, object[]>();

        var agrupados = result.GetFailures.OrderByDescending(a => (int)a.notificationType)
            .Select(a => new
            {
                key = a.member ?? nameof(NotificationType.BusinessNotification),
                message = a.message,
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

        return BadRequest(new
        {
            status = 400,
            errors = dic
        });
    }
}
