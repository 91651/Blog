using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry;
using OpenTelemetry.Trace;
using Telemetry;

namespace Microsoft.Extensions.DependencyInjection;

public static class TelemetryBuildExtensions
{
    public static TelemetryBuilder AddOpenTelemetryWithEFCoreExporter<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        services.AddOpenTelemetry().WithTracing(b =>
        {
            b.AddAspNetCoreInstrumentation(options =>
            {
                options.EnrichWithHttpResponse = (activity, request) =>
                {
                    var endpoint = request.HttpContext.GetEndpoint();
                    if (endpoint is not null && endpoint.Metadata.Any(m => m is ComponentTypeMetadata))
                    {
                        activity.SetTag("is.page_request", true);
                    }
                    var ip = request.Headers["X-Forwarded-For"].FirstOrDefault() ?? request.HttpContext.Connection.RemoteIpAddress?.ToString();
                    activity.SetTag("http.client_ip", ip);
                };
                options.EnrichWithHttpRequest = (activity, request) =>
                {
                    var endpoint = request.HttpContext.GetEndpoint();
                    if (endpoint != null)
                    {
                        activity.SetTag("is.page_request", true);
                    }
                    var ip = request.Headers["X-Forwarded-For"].FirstOrDefault() ?? request.HttpContext.Connection.RemoteIpAddress?.ToString();
                    activity.SetTag("http.client_ip", ip);
                };
            }).AddConsoleExporter().AddProcessor(sp => new BatchActivityExportProcessor(new EfCoreExporter(
                         sp.GetRequiredService<IServiceScopeFactory>())));
        });

        services.AddTelemetry(options =>
        {
            options.UseEntityFrameworkCore()
                   .UseDbContext<TContext>();
        });

        return new TelemetryBuilder(services);
    }
}