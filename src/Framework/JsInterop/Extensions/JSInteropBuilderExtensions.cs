using JSInterop.Options;
using Microsoft.Extensions.DependencyInjection;

namespace JSInterop;

public static class JSInteropBuilderExtensions
{
    public static IServiceCollection AddJSInterop(this IServiceCollection services)
    {
        services.AddSingleton(sp => new JSInteropOptions(sp, (options) => { }));
        services.AddScoped<IJSInteropModule, JSInteropModule>();

        return services;
    }
}

