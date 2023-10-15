using Architecture.Application.Domain.Models.Base;
using Azure;

namespace Architecture.WebApi.Structure.Extensions;

public static class RouteHandlerBuilderExtensions
{
    public static RouteHandlerBuilder Response<TResponseSucess>(this RouteHandlerBuilder route)
    {
        route.Produces<TResponseSucess>(StatusCodes.Status200OK)
           .Produces<ResponseError<Dictionary<object, object[]>>>(StatusCodes.Status400BadRequest);
        return route;
    }

    public static RouteHandlerBuilder Authorization(this RouteHandlerBuilder route, string Role = null)
    {
        if (!string.IsNullOrEmpty(Role))
        {
            route.RequireAuthorization(policy => policy.RequireRole(Role));
        }
        else
        {
            route.RequireAuthorization();
        }

        return route;
    }
}
