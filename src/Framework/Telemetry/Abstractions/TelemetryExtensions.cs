namespace Microsoft.Extensions.DependencyInjection;

public static class TelemetryExtensions
{
    public static TelemetryBuilder AddTelemetry(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        return new TelemetryBuilder(services);
    }

    public static IServiceCollection AddTelemetry(this IServiceCollection services, Action<TelemetryBuilder> configuration)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        configuration(services.AddTelemetry());

        return services;
    }
}