using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Telemetry.EntityFrameworkCore;

public class TelemetryEntityFrameworkCoreModelCustomizer : RelationalModelCustomizer
{
    public TelemetryEntityFrameworkCoreModelCustomizer(ModelCustomizerDependencies dependencies)
        : base(dependencies)
    {
    }

    public override void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        if (modelBuilder is null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        base.Customize(modelBuilder, context);
        modelBuilder.UseTelemetry();
    }
}