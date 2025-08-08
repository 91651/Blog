using System.Security.Claims;
using Blog.Admin.Data;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blog.Admin.Auth;

public class AppAuthenticationService : AuthenticationStateProvider
{
    private readonly ILogger _logger;
    public readonly IServiceProvider _serviceProvider;

    public AppAuthenticationService(ILogger<AppAuthenticationService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<AuthenticationState> GetAuthenticationStateIdentityAsync()
    {
        var identity = new ClaimsIdentity();
        var claims = await GetCurrentUser();
        if (claims.Any())
        {
            foreach (var claim in claims)
            {
                identity.AddClaim(new Claim(claim.Key, claim.Value));
            }
            identity = new ClaimsIdentity(identity.Claims, "Claims");
        }
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public Task<bool> SignIn()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        return Task.FromResult(true);
    }

    public async Task<bool> SignOut()
    {
        using var scope = _serviceProvider.CreateScope();
        var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
        var resp = await httpClient.PostAsync("/api/admin/auth/signout", null);
        if (resp.IsSuccessStatusCode)
        {
            return true;
        }
        return false;
    }

    private async Task<IEnumerable<KeyValuePair<string, string>>> GetCurrentUser()
    {
        using var scope = _serviceProvider.CreateScope();
        var Api = scope.ServiceProvider.GetRequiredService<IAdminApiProvider>();
        var resp = await Api.GetClaimsAsync();
        var claims = resp.Content ?? Enumerable.Empty<KeyValuePair<string, string>>();
        return claims;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() => GetAuthenticationStateIdentityAsync();
}