using Microsoft.EntityFrameworkCore;

namespace Telemetry.EntityFrameworkCore;

public static class OpenIddictEntityFrameworkCoreModelBuilderExtensions
{
    public static ModelBuilder UseTelemetry(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TelemetryEntityFrameworkCoreApplicationConfiguration());

        return builder;
    }
}