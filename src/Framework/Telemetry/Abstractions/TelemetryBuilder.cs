namespace Microsoft.Extensions.DependencyInjection;

public sealed class TelemetryBuilder
{
    public TelemetryBuilder(IServiceCollection services)
        => Services = services ?? throw new ArgumentNullException(nameof(services));

    public IServiceCollection Services { get; }
}