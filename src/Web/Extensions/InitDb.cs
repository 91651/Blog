﻿using Blog.Data;
using Blog.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Blog.Web.Extensions
{
    public static class InitDb
    {
        public static void UseInitDb(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                if (dbContext.Database.EnsureCreated())
                {
                    var initUser = InitUser(userManager);
                    Task.WaitAll(initUser);
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