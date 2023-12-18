using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace App.Blazor.Admin.Auth
{
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
            var httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
            var resp = await httpClient.GetAsync("/api/admin/auth/claims");
            if (resp.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions();
                var data = await resp.Content.ReadFromJsonAsync<IDictionary<string, string>>();
                return data;
            }
            return Enumerable.Empty<KeyValuePair<string, string>>();
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() => GetAuthenticationStateIdentityAsync();
    }
}
