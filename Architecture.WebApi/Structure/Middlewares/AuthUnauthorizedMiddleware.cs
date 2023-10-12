using Architecture.Application.Core.Structure;
using Architecture.Application.Domain.Models.Base;
using System.Text.Json;

namespace Architecture.WebApi.Structure.Middlewares;

public class AuthUnauthorizedMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;
    private IEnumerable<int> _vallowedStatusCodes = new int[] { StatusCodes.Status401Unauthorized, StatusCodes.Status403Forbidden };

    public AuthUnauthorizedMiddleware(RequestDelegate next,
        AppSettings appSettings
    )
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        await _next(httpContext);
        await WriteUnauthorizedResponseAsync(httpContext);
    }

    public async Task WriteUnauthorizedResponseAsync(HttpContext httpContext)
    {
        if (_vallowedStatusCodes.Contains(httpContext.Response.StatusCode) is false)
            return;

        var statusCode = httpContext.Response.StatusCode;
        var errormodel = new ResponseError<ErrorsModel>();

        switch (statusCode)
        {
            case StatusCodes.Status401Unauthorized:
                errormodel = new ResponseError<ErrorsModel>
                {
                    HttpCode = StatusCodes.Status401Unauthorized,
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Message = "Acesso negado. Você não tem permissões suficientes para acessar esta API",
                            Context = "Authorization"
                        }
                    }
                };
                break;
            case StatusCodes.Status403Forbidden:
                errormodel = new ResponseError<ErrorsModel>
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Message = "Não autorizado. Credenciais fornecidas ausentes, inválidas ou expiradas",
                            Context = "Authorization"
                        }
                    }
                };
                break;
        }

        httpContext.Response.ContentType = "application/json";

        await httpContext.Response
            .WriteAsync(JsonSerializer.Serialize(errormodel));
    }
}
