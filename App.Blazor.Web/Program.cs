using App.Blazor.Web.Data;
using App.DbAccess.Entities.Identity;
using App.DbAccess.Infrastructure;
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
    //�����û��������
    options.Password = new PasswordOptions
    {
        RequiredLength = 6
    };
});

services.AddControllersWithViews();
services.AddDatabaseDeveloperPageExceptionFilter();
services.AddRazorPages();
services.AddServerSideBlazor();
services.AddAntDesign();
services.AddAuthentication();
services.AddEndpointsApiExplorer();
services.AddRouting(options => options.LowercaseUrls = true);
services.AddOpenApiDocument();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("admin", "admin/index.html");

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();