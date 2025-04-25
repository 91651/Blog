using System.Text.Encodings.Web;
using System.Text.Unicode;
using AntDesign.ProLayout;
using Blog.Admin;
using Blog.Admin.Auth;
using Blog.Admin.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var services = builder.Services;
services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
services.AddHttpClient(nameof(Blog.Admin), client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<AppAuthorizationMessageHandler>();
services.AddRefitClient<IAdminApiProvider>().ConfigureHttpClient(c =>
{
    var uri = new Uri(builder.HostEnvironment.BaseAddress);
    var baseUrl = uri.GetLeftPart(UriPartial.Authority);
    c.BaseAddress = new Uri(baseUrl);
}).AddHttpMessageHandler<AppAuthorizationMessageHandler>();
services.AddTransient<AppAuthorizationMessageHandler>();
services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(Blog.Admin)));
services.AddAuthorizationCore();
services.AddCascadingAuthenticationState();
services.AddSingleton<AppAuthenticationService>();
services.AddSingleton<AuthenticationStateProvider>(p => p.GetRequiredService<AppAuthenticationService>());
services.AddAntDesign();
services.Configure<ProSettings>(x =>
{
    x.Title = "内容管理后台";
    x.NavTheme = "light";
    x.Layout = "mix";
    x.PrimaryColor = "daybreak";
    x.ContentWidth = "Fluid";
    x.HeaderHeight = 64;
});

await builder.Build().RunAsync();