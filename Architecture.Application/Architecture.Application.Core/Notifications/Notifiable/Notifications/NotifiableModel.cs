using Architecture.Application.Core.Notifications.Annotations;
using Architecture.Application.Core.Notifications.Context;
using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{
    [JsonIgnore]
    protected Result Result { get; set; }

    [JsonIgnore]
    protected PropInfo CurrentProp { get; set; }

    public Notifiable()
    {
        Result = new Result(new NotificationContext());
        CurrentProp = new PropInfo();
    }

    [JsonIgnore]
    protected EntityInfo EntityInfo => new EntityInfo()
    {
        Name = typeof(TEntity).Name,
        Namespace = typeof(TEntity).Namespace
    };

    public List<NotificationModel> GetFailures()
    {
        return Result.GetContext().Notifications.ToList();
    }

    public bool HasFailure()
    {
        return GetFailures().Any();
    }

    private void SetValue(string func, dynamic value)
    {
        string member = func.Split("=>")[0].Trim();
        List<ValidationResult> validationResults = new List<ValidationResult>();

        try
        {
            var prop = this.GetType().GetProperty(member);
            prop.SetValue(this, value);

            CurrentProp = new PropInfo()
            {
                MemberName = value is INotifiableModel ? EntityInfo.Name : string.Concat(EntityInfo.Name, ".", member),
                Value = value
            };

            Validator.TryValidateProperty(value, new ValidationContext(this, null, null) { MemberName = member }, validationResults);

            if (validationResults.Any())
            {
                var atttr = prop.GetCustomAttributes<ValidationAttribute>();

                foreach(IFailureAttribute att in atttr)
                {
                    if (!att.IsValid(value))
                    {
                        Result.Failure(new NotificationModel(
                           new FailureModel(att.ErrorCode, att.ErrorMessage),
                           new NotificationInfo(CurrentProp, EntityInfo)
                        ));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}