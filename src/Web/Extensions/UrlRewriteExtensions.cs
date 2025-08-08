using System.Text.RegularExpressions;
using Blog.Data;
using Microsoft.AspNetCore.Rewrite;

namespace Blog.Web.Extensions;

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

public class RedirectRule : IRule
{
    private readonly TimeSpan _regexTimeout = TimeSpan.FromSeconds(1);
    public Regex InitialMatch { get; }
    public string Replacement { get; }
    public int StatusCode { get; }

    public RedirectRule(string regex, string replacement, int statusCode)
    {
        ArgumentException.ThrowIfNullOrEmpty(regex);
        ArgumentException.ThrowIfNullOrEmpty(replacement);

        InitialMatch = new Regex(regex, RegexOptions.Compiled | RegexOptions.CultureInvariant, _regexTimeout);
        Replacement = replacement;
        StatusCode = statusCode;
    }

    public void ApplyRule(RewriteContext context)
    {
        var request = context.HttpContext.Request;
        var path = request.Path;
        var pathBase = request.PathBase;

        Match initMatchResults;
        if (!path.HasValue)
        {
            initMatchResults = InitialMatch.Match(string.Empty);
        }
        else
        {
            initMatchResults = InitialMatch.Match(path.ToString().Substring(1));
        }
        Microsoft.AspNetCore.Rewrite.RedirectRule(
    }
}