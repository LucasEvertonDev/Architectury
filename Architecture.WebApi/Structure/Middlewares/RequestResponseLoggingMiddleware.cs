using Newtonsoft.Json;
using Serilog.Context;
using System.Text;

namespace Architecture.WebApi.Structure.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Read and log request body data
        string requestBodyPayload = await ReadRequestBody(context.Request);

        var httpRequestInfo = new
        {
            Host = context.Request.Host.ToString(),
            context.Request.Path,
            context.Request.Scheme,
            context.Request.Method,
            context.Request.Protocol,
            QueryString = context.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString()),
            Headers = context.Request.Headers
                .Where(x => x.Key != "Cookie" && x.Key != "Authorization") // remove Cookie from header since it is analysed separatly
                .ToDictionary(x => x.Key, y => y.Value.ToString()),
            Cookies = context.Request.Cookies.ToDictionary(x => x.Key, y => y.Value.ToString()),
            Body = requestBodyPayload
        };

        LogContext.PushProperty("Request", JsonConvert.SerializeObject(httpRequestInfo));

        await _next(context);
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        request.EnableBuffering();

        var body = request.Body;
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer, 0, buffer.Length);
        string requestBody = Encoding.UTF8.GetString(buffer);
        body.Seek(0, SeekOrigin.Begin);
        request.Body = body;

        return requestBody?.Replace("\n", string.Empty)?.Replace("\"", "");
    }
}
