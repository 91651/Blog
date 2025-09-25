using Microsoft.EntityFrameworkCore;
using Telemetry.EntityFrameworkCore.Entity;

namespace Telemetry.EntityFrameworkCore;

public interface ITelemetryEntityFrameworkCoreContext
{
    ValueTask<DbContext> GetDbContextAsync(CancellationToken cancellationToken);

    Task AddUserTraceAsync(UserTrace UserTrace);

    Task AddUserTracesAsync(IEnumerable<UserTrace> userTraces);
}