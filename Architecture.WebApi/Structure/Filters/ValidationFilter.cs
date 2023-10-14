using Architecture.Application.Domain.Models.Base;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

namespace Architecture.WebApi.Structure.Filters;

public class ValidationFilter : IEndpointFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (!context.Arguments.Any())
        {
            return await next(context);
        }

        var validationFailures = new List<ValidationFailure>();

        foreach (var actionArgument in context.Arguments)
        {
            if (actionArgument.GetType().GetInterface(nameof(IValidationAsync)) != null)
            {
                var validationErrors = await GetValidationErrorsAsync(actionArgument);
                validationFailures.AddRange(validationErrors);
            }
        }

        if (!validationFailures.Any())
        {
            return await next(context);
        }

        return Results.BadRequest(validationFailures.ToProblemDetails());
    }


    private async Task<IEnumerable<ValidationFailure>> GetValidationErrorsAsync(object value)
    {

        if (value == null)
        {
            return new[] { new ValidationFailure("", "instance is null") };
        }

        var validatorInstance = GetValidatorFor(value.GetType());
        if (validatorInstance == null)
        {
            return new List<ValidationFailure>();
        }

        var validationResult = await validatorInstance.ValidateAsync(new ValidationContext<object>(value));
        return validationResult.Errors ?? new List<ValidationFailure>();
    }

    private IValidator GetValidatorFor(Type type)
    {
        var genericValidatorType = typeof(IValidator<>);
        var specificValidatorType = genericValidatorType.MakeGenericType(type);

        var validatorInstance = (IValidator)_serviceProvider.GetService(specificValidatorType);
        return validatorInstance;
    }
}

public static class ValidationResultExtensions
{
    public static ProblemDetails ToProblemDetails(this IEnumerable<ValidationFailure> validationFailures)
    {
        var errors = validationFailures.ToDictionary(x => x.PropertyName, x => x.ErrorMessage);

        var problemDetails = new ProblemDetails
        {
            Type = "ValidationError",
            Detail = "invalid request, please check the error list for more details",
            Status = (int)HttpStatusCode.BadRequest,
            Title = "invalid request"
        };

        problemDetails.Extensions.Add("errors", errors);
        return problemDetails;
    }
}
