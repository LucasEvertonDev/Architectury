using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Core.Notifications.Services;
 
namespace Architecture.WebApi.Structure.Extensions;

public static class ResultExtensions
{
    public static IResult BadRequestFailure(this Result result)
    {
        return Results.BadRequest(new ResponseError<Dictionary<object, object[]>>
        {
            HttpCode = 400,
            Success = false,
            Errors = ResultService.GetFailures(result)
        });
    }

    public static Task<IResult> GetResponse(this Result result)
    {
        if (result.HasFailures())
        {
            return Task.FromResult(Results.BadRequest(new ResponseError<Dictionary<object, object[]>>
            {
                HttpCode = 400,
                Success = false,
                Errors = ResultService.GetFailures(result)
            }));
        }
        else
        {
            if (result.GetContent<dynamic>() == null)
            {
                return Task.FromResult(Results.Ok(new ResponseDto
                {
                    Success = true
                }));
            }
            else
            {
                return Task.FromResult(Results.Ok(new ResponseDto<dynamic>()
                {
                    Content = result.GetContent<dynamic>()
                }));
            }
        }
    }
}
