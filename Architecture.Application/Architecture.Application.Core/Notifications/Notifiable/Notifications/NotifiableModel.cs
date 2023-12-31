﻿using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using System.Text.Json.Serialization;

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

        try
        {
            this.GetType().GetProperty(member).SetValue(this, value);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        CurrentProp = new PropInfo()
        {
            MemberName = value is INotifiableModel ? EntityInfo.Name : string.Concat(EntityInfo.Name, ".", member),
            Value = value
        };
    }
}