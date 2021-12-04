using System.Net.Http.Json;
using System.Security.Claims;
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

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
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
            var state = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
            NotifyAuthenticationStateChanged(state);
            return await state;
        }
        public Task<bool> SignIn()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return Task.FromResult(true);
        }
        private async Task<IEnumerable<KeyValuePair<string, string>>> GetCurrentUser()
        {
            var httpClient = _serviceProvider.GetService<HttpClient>();
            var resp = await httpClient.GetAsync("/api/admin/auth/claims");
            if (resp.IsSuccessStatusCode)
            {
                var data = await resp.Content.ReadFromJsonAsync<IEnumerable<KeyValuePair<string, string>>>();
                return data;
            }
            return Enumerable.Empty<KeyValuePair<string, string>>();
        }
    }
}
