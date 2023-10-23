using App.Blazor.Client.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var services = builder.Services;
services.AddRefitClient<IDataProviderApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
services.AddHotKeys2();
services.AddAntDesign();

await builder.Build().RunAsync();
