using System.Reflection;
using System.Runtime.CompilerServices;
using AntDesign.ProLayout;
using App.Blazor.Admin;
using App.Blazor.Admin.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<_App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient(nameof(App.Blazor.Admin), client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<AppAuthorizationMessageHandler>();
builder.Services.AddTransient<AppAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(App.Blazor.Admin)));
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AppAuthenticationService>();
builder.Services.AddSingleton<AuthenticationStateProvider>(p => p.GetRequiredService<AppAuthenticationService>());
builder.Services.AddAntDesign();
builder.Services.Configure<ProSettings>(x =>
{
    x.Title = "内容管理后台";
    x.NavTheme = "light";
    x.Layout = "mix";
    x.PrimaryColor = "daybreak";
    x.ContentWidth = "Fluid";
    x.HeaderHeight = 64;
});

await builder.Build().RunAsync();
