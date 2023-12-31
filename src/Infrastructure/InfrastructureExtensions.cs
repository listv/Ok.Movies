﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Ok.Movies.Infrastructure.Authentication;
using Ok.Movies.Infrastructure.Cache;
using Ok.Movies.Infrastructure.Database;
using Ok.Movies.Infrastructure.Health;
using Ok.Movies.Infrastructure.Middleware;
using Ok.Movies.Infrastructure.OpenApi;
using Ok.Movies.Infrastructure.Versioning;

namespace Ok.Movies.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        services
            .AddExceptionMiddleware(environment)
            .AddDatabase()
            .AddJwtAuthentication()
            .AddAppAuthorization()
            .AddVersioning()
            .AddOpenApiDocumentation()
            .AddHealthCheck()
            // .AddResponseCaching()
            .AddOutputCaching()
            ;

        return services;
    }

    public static IApplicationBuilder? UseInfrastructure(this IApplicationBuilder builder)
    {
        return builder
            .UseAuthentication()
            .UseAuthorization()
            // .UseCors()
            // .UseResponseCaching()
            .UseOutputCaching()
            .UseExceptionMiddleware()
            .UseOpenApiDocumentation();
    }

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapControllers();
        builder.MapHealthCheck();
        return builder;
    }
}
