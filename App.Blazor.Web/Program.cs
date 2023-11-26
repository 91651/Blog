using System.Text.Encodings.Web;
using System.Text.Unicode;
using App.Blazor.Client.Data;
using App.Blazor.Web.Components;
using App.Business.Services.AutoMapper;
using App.DbAccess.Entities.Identity;
using App.DbAccess.Infrastructure;
using App.DbAccess.Repositories;
using BC.Microsoft.DependencyInjection.Plus;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Refit;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
var services = builder.Services;

services.AddRazorComponents().AddInteractiveServerComponents().AddInteractiveWebAssemblyComponents();
services.AddDbContext<AppDbContext>(option => option.UseSqlite(configuration["ConnectionStrings:SqliteConnection"]));
services.AddDefaultIdentity<User>().AddEntityFrameworkStores<AppDbContext>();
services.Configure<IdentityOptions>(options =>
{
    //配置用户密码策略
    options.Password = new PasswordOptions
    {
        RequiredLength = 6
    };
});
services.ConfigureApplicationCookie(o =>
{
    o.Cookie.MaxAge = TimeSpan.FromDays(1);
    o.Cookie.SameSite = SameSiteMode.None;
    o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    o.Cookie.HttpOnly = true;
});
services.AddMemoryCache();
services.AddControllersWithViews(opt =>
{
    opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
});
services.AddDatabaseDeveloperPageExceptionFilter();
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
services.AddAuthentication().AddCookie(o =>
{
    o.Events.OnRedirectToLogin = (context) =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});
services.AddEndpointsApiExplorer();
services.AddRouting(options => options.LowercaseUrls = true);
services.AddOpenApiDocument();
services.AddCors(options => options.AddDefaultPolicy(builder => builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
services.AddAutoMapper(options => options.AddProfile<Mappings>());
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
services.AddScopedFromAssembly(nameof(App.Business.Services), o => o.Matching = true);

services.AddAntDesign();
services.AddRefitClient<IDataProviderApi>().ConfigureHttpClient((sp, c) => {
    var server = sp.GetRequiredService<IServer>();
    var addressFeature = server.Features.Get<IServerAddressesFeature>();
    var baseAddress = addressFeature.Addresses.Last();
    c.BaseAddress = new Uri(baseAddress);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
    app.UseOpenApi();
    app.UseSwaggerUi();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseInitDb();
app.UseHttpsRedirection();
app.UseRewriter(new RewriteOptions().AddRedirectToNonWww());
app.UseRewriter(new RewriteOptions().AddRewrite(@"^.+/_content/(.*)", "_content/$1", skipRemainingRules: true));
app.UseBlazorFrameworkFiles("/admin");
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions { RequestPath = "/static", FileProvider = new PhysicalFileProvider(Directory.CreateDirectory(Path.GetFullPath(configuration["AppSettings:StaticContentPath"])).FullName) });
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapControllers();
app.MapFallbackToFile("admin/{*path:nonfile}", "admin/index.html");

app.MapRazorComponents<_App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(App.Blazor.Client._Imports).Assembly);

app.Run();
