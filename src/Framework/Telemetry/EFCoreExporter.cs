using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using Telemetry.EntityFrameworkCore;
using Telemetry.EntityFrameworkCore.Entity;

namespace Telemetry;

public class EfCoreExporter : BaseExporter<Activity>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public EfCoreExporter(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public override ExportResult Export(in Batch<Activity> batch)
    {
        UserTraceExport(batch);
        return ExportResult.Success;
    }

    private async void UserTraceExport(Batch<Activity> batch)
    {
        List<UserTrace> list = new();
        foreach (var activity in batch)
        {
            if (activity.GetTagItem("is.page_request") as bool? == true)
            {
                var t = new UserTrace
                {
                    Id = Guid.CreateVersion7().ToString(),
                    TraceId = activity.TraceId.ToString(),
                    StartTime = activity.StartTimeUtc,
                    Duration = activity.Duration,
                    Server = activity.GetTagItem("server.address")?.ToString(),
                    Port = int.TryParse(activity.GetTagItem("server.port")?.ToString(), out var port) ? port : null,
                    Method = activity.GetTagItem("http.request.method")?.ToString(),
                    Scheme = activity.GetTagItem("url.scheme")?.ToString(),
                    Path = activity.GetTagItem("url.path")?.ToString(),
                    ProtocolVersion = activity.GetTagItem("network.protocol.version")?.ToString(),
                    UserAgent = activity.GetTagItem("user_agent.original")?.ToString(),
                    ClientIp = activity.GetTagItem("http.client_ip")?.ToString(),
                    StatusCode = int.TryParse(activity.GetTagItem("http.response.status_code")?.ToString(), out var code) ? code : null,
                };
                list.Add(t);
            }
        }
        if (list.Count > 0)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ITelemetryEntityFrameworkCoreContext>();
            await dbContext.AddUserTracesAsync(list);
        }
    }
}