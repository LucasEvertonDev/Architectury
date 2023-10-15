using System.Text.Json.Serialization;

namespace Architecture.Application.Domain.Models.Base;

public class ResponseDto<TResult>
{
    public bool Success { get; set; } = true;
    public int HttpCode { get; set; } = 200;
    public TResult Content { get; set; }
}

public class ResponseDto
{
    public bool Success { get; set; } = true;
    public int HttpCode { get; set; } = 200;
}

public class ResponseError<TResult>
{
    public bool Success { get; set; } = false;
    public int HttpCode { get; set; }
    public TResult Errors { get; set; }
}

