namespace Architecture.Application.Core.Notifications.Annotations;

public interface IFailureAttribute
{
    public string ErrorCode { get; set; }

    public string ErrorMessage { get; set; }

    public bool IsValid(object? value);
}
