using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telemetry.EntityFrameworkCore.Entity;

namespace Telemetry.EntityFrameworkCore;

public class TelemetryEntityFrameworkCoreApplicationConfiguration
    : IEntityTypeConfiguration<UserTrace>
{
    public void Configure(EntityTypeBuilder<UserTrace> builder)
    {
    }
}