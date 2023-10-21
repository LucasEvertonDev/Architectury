using System.ComponentModel.DataAnnotations;

namespace Architecture.Application.Core.Notifications.Annotations;

public class CustomRequiredAttribute : RequiredAttribute, IFailureAttribute
{
    public string ErrorCode { get; set; }

    public string ErrorMessage { get; set; }

    public CustomRequiredAttribute()
    {
    }
}
