using App.Blazor.Web.Data;
using App.Business.Services.AutoMapper;
using App.DbAccess.Entities.Identity;
using App.DbAccess.Infrastructure;
using App.DbAccess.Repositories;
using BC.Microsoft.DependencyInjection.Plus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
var services = builder.Services;

services.AddDbContext<AppDbContext>(option => option.UseSqlite(configuration["ConnectionStrings:SqliteConnection"]));
services.AddDefaultIdentity<User>().AddEntityFrameworkStores<AppDbContext>();
services.Configure<IdentityOptions>(options =>
{
    //ÅäÖÃÓÃ»§ÃÜÂë²ßÂÔ
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

services.AddControllersWithViews();
services.AddDatabaseDeveloperPageExceptionFilter();
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddAntDesign();
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
services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
    app.UseOpenApi();
    app.UseSwaggerUi3();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseInitDb();
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("admin", "admin/index.html");

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
