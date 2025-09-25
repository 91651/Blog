using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Telemetry.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class TelemetryEntityFrameworkCoreExtensions
{
    public static void UseTelemetry(this DbContextOptionsBuilder options)
    {
        options.ReplaceService<IModelCustomizer, TelemetryEntityFrameworkCoreModelCustomizer>();
    }

    public static TelemetryEntityFrameworkCoreBuilder UseEntityFrameworkCore(this TelemetryBuilder builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.TryAddScoped<ITelemetryEntityFrameworkCoreContext>();

        return new TelemetryEntityFrameworkCoreBuilder(builder.Services);
    }

    public static TelemetryBuilder UseEntityFrameworkCore(
        this TelemetryBuilder builder, Action<TelemetryEntityFrameworkCoreBuilder> configuration)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        if (configuration is null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        configuration(builder.UseEntityFrameworkCore());

        return builder;
    }
}