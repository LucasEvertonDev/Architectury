using Architecture.Application.Domain.Models.Base;
using Architectury.Infra.Plugins.FluentValidation.Structure.Service;
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
        var validationFailures = new List<ValidationFailure>();

        if (!context.Arguments.Any())
        {
            return await next(context);
        }

        foreach (var actionArgument in context.Arguments)
        {
            //validationFailures.AddRange(await new FluentService(_serviceProvider).ValidateParameterAsync(actionArgument));
        }

        if (!validationFailures.Any())
        {
            return await next(context);
        }

        return Results.BadRequest(validationFailures.ToProblemDetails());
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
