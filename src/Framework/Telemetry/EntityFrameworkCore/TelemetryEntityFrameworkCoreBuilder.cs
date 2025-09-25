using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Telemetry.EntityFrameworkCore
{
    public sealed class TelemetryEntityFrameworkCoreBuilder
    {
        public IServiceCollection Services { get; }

        public TelemetryEntityFrameworkCoreBuilder(IServiceCollection services)
        => Services = services ?? throw new ArgumentNullException(nameof(services));

        public TelemetryEntityFrameworkCoreBuilder UseDbContext<TContext>() where TContext : DbContext
        {
            Services.Replace(ServiceDescriptor.Scoped<
            ITelemetryEntityFrameworkCoreContext, TelemetryEntityFrameworkCoreContext<TContext>>());

            return this;
        }
    }
}