using App.DbAccess.Entities.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace App.DbAccess.Infrastructure
{
    public static class InitDb
    {
        public static async Task UseInitDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                if (dbContext.Database.EnsureCreated())
                {
                    await InitUser(userManager);
                }
            }
        }
        public static async Task InitUser(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    Email = "mail@521.org.cn",
                    UserName = "admin"
                };
                var result = await userManager.CreateAsync(user, "Qwer1234!");
            }
        }
    }
}
