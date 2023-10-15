using Architecture.Application.Core.Notifications.Enum;
using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Base;

namespace Architecture.WebApi.Structure.Extensions;

public static class ResultExtensions
{
    public static IResult BadRequestFailure(this Result result)
    {
        Dictionary<object, object[]> dic = new Dictionary<object, object[]>();

        var agrupados = result.GetFailures.OrderByDescending(a => (int)a.NotificationInfo.EntityInfo.NotificationType)
            .Select(a => new
            {
                key = a.NotificationInfo.PropInfo.MemberName ?? nameof(NotificationType.BusinessNotification),
                a.Error.message,
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

        return Results.BadRequest(new
        {
            status = 400,
            errors = dic
        });
    }

    public static IResult GetResponse<T>(this Result result)
    {
        if (result.HasFailures())
        {
            Dictionary<object, object[]> dic = new Dictionary<object, object[]>();

            var agrupados = result.GetFailures.OrderByDescending(a => (int)a.NotificationInfo.EntityInfo.NotificationType)
                .Select(a => new
                {
                    key = a.NotificationInfo.PropInfo.MemberName ?? nameof(NotificationType.BusinessNotification),
                    a.Error.message,
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

            return Results.BadRequest(new
            {
                status = 400,
                errors = dic
            });
        }
        else
        {
            if (typeof(T) == typeof(ResponseDto))
            {
                return Results.Ok(new ResponseDto
                {
                    Success = true
                });
            }
            else
            {
                return Results.Ok(new ResponseDto<T>()
                {
                    Content = result.GetValue<T>()
                });
            }
        }
    }
}
