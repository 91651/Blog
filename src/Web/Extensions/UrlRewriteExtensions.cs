using Blog.Data;
using Microsoft.AspNetCore.Rewrite;

namespace Blog.Web.Extensions
{
    public static class UrlRewriteExtensions
    {
        public static RewriteOptions LoadUrlRewireRelusAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var rules = db.UrlRewriteRules.ToList();
            var options = new RewriteOptions();
            foreach (var rule in rules)
            {
                options.AddRedirect(rule.Regex, rule.Replacement, int.Parse(rule.StatusCode));
            }

            return options;
        }

        public static void UseUrlRewrite(this IApplicationBuilder app)
        {
            var rewriteOptions = LoadUrlRewireRelusAsync(app);
            app.UseRewriter(rewriteOptions);
        }
    }
}