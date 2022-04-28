using App.Blazor.Client.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var services = builder.Services;

await builder.Build().RunAsync();

public static class AppExtensions
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddRefitClient<IDataProviderApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
    }
}
