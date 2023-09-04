﻿using Infrastructure.Database;
using Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IWebHostEnvironment environment, string[] args)
    {
        return services
            .AddExceptionMiddleware(environment)
            .AddDatabase(args);
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
    {
        return builder
            .UseExceptionMiddleware();
    }
}
