using AntDesign.ProLayout;
using App.Blazor.Admin;
using App.Blazor.Admin.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Application>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress = new Uri("https://localhost:44326/"))
    .AddHttpMessageHandler<AppAuthorizationMessageHandler>();
builder.Services.AddTransient<AppAuthorizationMessageHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));
builder.Services.AddScoped<AppAuthenticationService>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetService<AppAuthenticationService>());

builder.Services.AddAuthorizationCore();
builder.Services.AddAntDesign();
builder.Services.Configure<ProSettings>(x =>
{
    x.Title = "Ant Design Pro";
    x.NavTheme = "light";
    x.Layout = "mix";
    x.PrimaryColor = "daybreak";
    x.ContentWidth = "Fluid";
    x.HeaderHeight = 64;
});

await builder.Build().RunAsync();
