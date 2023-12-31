﻿using Architecture.Application.Core.Structure;
using Architecture.Application.Mediator.Pipelines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Application.Mediator;

public static class BootstrapModule
{
    public static void RegisterMediatR(this IServiceCollection services, AppSettings configuration)
    {
        services.AddMediatR(typeof(BootstrapModule).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehaviour<,>));
    }
}
