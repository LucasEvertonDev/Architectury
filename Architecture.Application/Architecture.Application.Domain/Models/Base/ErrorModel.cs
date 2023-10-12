namespace Architecture.Application.Domain.Models.Base;

public class ErrorModel
{
    public ErrorModel()
    {
    }

    public ErrorModel(string message, string context)
    {
        Message = message;
        Context = context;
    }

    public string Message { get; set; }
    public string Context { get; set; }
}
