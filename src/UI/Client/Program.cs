using Blog.Client.Data;
using Blog.Client.Shared;
using JSInterop;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var services = builder.Services;
services.AddRefitClient<IClientApiProvider>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
services.AddClientServices();

await builder.Build().RunAsync();

public static class ServiceExtensions
{
    public static void AddClientServices(this IServiceCollection services)
    {
        services.AddHotKeys2();
        services.AddScoped<MaskService>();
        services.AddJSInterop();
    }
}