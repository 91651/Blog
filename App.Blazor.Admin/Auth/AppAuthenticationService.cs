using System.Net.Http.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace App.Blazor.Admin.Auth
{
    public class AppAuthenticationService : AuthenticationStateProvider
    {
        public bool IsAuthenticated = false;
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
            if (IsAuthenticated)
            {
                var claims = await GetCurrentUser();
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
            IsAuthenticated = true;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return Task.FromResult(true);
        }
        private async Task<IEnumerable<KeyValuePair<string, string>>> GetCurrentUser()
        {
            var httpClient = _serviceProvider.GetService<HttpClient>();
            var resp = await httpClient.GetFromJsonAsync<IEnumerable<KeyValuePair<string, string>>>("/api/admin/auth/claims");
            return resp;
        }
    }
}
