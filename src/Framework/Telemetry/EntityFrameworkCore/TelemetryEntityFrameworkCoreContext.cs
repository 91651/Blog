using Microsoft.EntityFrameworkCore;
using Telemetry.EntityFrameworkCore.Entity;

namespace Telemetry.EntityFrameworkCore;

public sealed class TelemetryEntityFrameworkCoreContext<TContext> : ITelemetryEntityFrameworkCoreContext
    where TContext : DbContext
{
    public readonly TContext _context;

    public TelemetryEntityFrameworkCoreContext(TContext context)
    {
        _context = context;
    }

    public ValueTask<DbContext> GetDbContextAsync(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new(Task.FromCanceled<DbContext>(cancellationToken));
        }

        if (_context is not DbContext context)
        {
            return new(Task.FromException<DbContext>(new InvalidOperationException()));
        }

        return new(context);
    }

    public async Task AddUserTraceAsync(UserTrace userTrace)
    {
        await _context.Set<UserTrace>().AddAsync(userTrace);
        await _context.SaveChangesAsync();
    }

    public async Task AddUserTracesAsync(IEnumerable<UserTrace> userTraces)
    {
        await _context.Set<UserTrace>().AddRangeAsync(userTraces);
        await _context.SaveChangesAsync();
    }
}