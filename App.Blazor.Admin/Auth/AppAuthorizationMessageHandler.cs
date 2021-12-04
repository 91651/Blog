using System.Net;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace App.Blazor.Admin.Auth
{
    public class AppAuthorizationMessageHandler : DelegatingHandler, IDisposable
    {
        private readonly AppAuthenticationService _appAuthenticationService;
        private readonly NavigationManager _navigation;

        public AppAuthorizationMessageHandler(AppAuthenticationService appAuthenticationService, NavigationManager navigation)
        {
            _appAuthenticationService = appAuthenticationService;
            _navigation = navigation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            var httpResponseMessage = await base.SendAsync(request, cancellationToken);
            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigation.NavigateTo("/admin/signin");
            }
            return httpResponseMessage;
        }
    }
}
