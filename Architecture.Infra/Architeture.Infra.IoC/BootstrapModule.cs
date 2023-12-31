﻿using Architecture.Application.Core;
using Architecture.Application.Core.Structure;
using Architecture.Application.Mediator;
using Architecture.Infra.Data;
using Architectury.Infra.Plugins;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Infra.IoC;

public static class BootstrapModule
{
    public static void AddInfraStructure(this IServiceCollection services, AppSettings configuration)
    {
        services.AddHttpContextAccessor();

        services.RegisterCore(configuration);

        services.RegisterMediatR(configuration);

        services.RegisterInfraData(configuration);

        services.RegisterPlugins(configuration);
    }
}
