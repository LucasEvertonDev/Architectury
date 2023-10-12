
namespace Architecture.Application.Domain.Models.Base;
public class ErrorsModel
{
    public ErrorsModel() { }

    public ErrorModel[] Messages { get; set; }

    public ErrorsModel(params ErrorModel[] message)
    {
        Messages = message;
    }
}
