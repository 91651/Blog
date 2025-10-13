using System.Text.Encodings.Web;
using System.Text.Unicode;
using BC.Microsoft.DependencyInjection.Plus;
using Blog.Client.Data;
using Blog.Data;
using Blog.Data.Entities.Identity;
using Blog.Web.Components;
using Blog.Web.Extensions;
using Blog.Web.Extensions.AutoMapper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Refit;
using Telemetry.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
var services = builder.Services;

services.AddRazorComponents().AddInteractiveServerComponents().AddInteractiveWebAssemblyComponents();
services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlite(configuration["ConnectionStrings:SqliteConnection"]);
    option.UseTelemetry();
});
services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

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
    o.Events.OnRedirectToLogin = (context) =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
    o.Events.OnRedirectToAccessDenied = (context) =>
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    };
});
services.AddMemoryCache();
services.AddControllersWithViews(opt =>
{
    opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
});
services.AddDistributedMemoryCache();
// services.AddOpenTelemetryWithEFCoreExporter<AppDbContext>();

services.AddDatabaseDeveloperPageExceptionFilter();
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
services.AddAuthentication().AddCookie();
services.AddEndpointsApiExplorer();
services.AddRouting(options => options.LowercaseUrls = true);
services.AddOpenApiDocument();
services.AddCors(options => options.AddDefaultPolicy(builder => builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
services.AddAutoMapper(options => options.AddProfile<Mappings>());
services.AddScopedFromAssembly(nameof(Blog.Service), o => o.Matching = true);

builder.Services.AddSlideCaptcha(builder.Configuration);

services.AddRefitClient<IClientApiProvider>().ConfigureHttpClient((sp, c) =>
{
    var server = sp.GetRequiredService<IServer>();
    var addressFeature = server.Features.Get<IServerAddressesFeature>();
    var baseAddress = addressFeature.Addresses.Last();
    c.BaseAddress = new Uri(baseAddress);
});

services.AddClientServices();

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
app.UseStaticFiles(new StaticFileOptions { RequestPath = "/static", FileProvider = new PhysicalFileProvider(Directory.CreateDirectory(Path.GetFullPath(configuration["AppSettings:StaticContentPath"]!)).FullName) });
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapControllers();
app.MapFallbackToFile("admin/{*path:nonfile}", "admin/index.html");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Blog.Client._Imports).Assembly);

app.Run();