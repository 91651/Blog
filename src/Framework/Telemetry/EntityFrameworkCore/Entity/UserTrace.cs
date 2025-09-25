using System.ComponentModel.DataAnnotations;

namespace Telemetry.EntityFrameworkCore.Entity;

public class UserTrace
{
    [Key]
    public string Id { get; set; }

    [MaxLength(40)]
    public string TraceId { get; set; }

    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }

    [MaxLength(255)]
    public string Server { get; set; }

    public int? Port { get; set; }

    [MaxLength(10)]
    public string Method { get; set; }

    [MaxLength(10)]
    public string Scheme { get; set; }

    [MaxLength(2000)]
    public string Path { get; set; }

    [MaxLength(10)]
    public string ProtocolVersion { get; set; }

    [MaxLength(2000)]
    public string UserAgent { get; set; }

    [MaxLength(45)]
    public string ClientIp { get; set; }

    public int? StatusCode { get; set; }
}