using App.DbAccess.Entities.Identity;
using App.DbAccess.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
var services = builder.Services;
services.AddControllers();
services.AddCors(options => options.AddDefaultPolicy(builder => builder.SetIsOriginAllowed(origin => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()));
services.AddDbContext<AppDbContext>(option => option.UseSqlite(configuration["ConnectionStrings:SqliteConnection"]));
services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>();
services.Configure<IdentityOptions>(options =>
{
    //ÅäÖÃÓÃ»§ÃÜÂë²ßÂÔ
    options.Password = new PasswordOptions
    {
        RequiredLength = 6
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddRouting(options => options.LowercaseUrls = true);
services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseInitDb();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
