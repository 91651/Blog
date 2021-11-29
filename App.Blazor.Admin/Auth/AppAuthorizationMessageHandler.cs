using System.Net;
using Microsoft.AspNetCore.Components;

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
            var httpResponseMessage = await base.SendAsync(request, cancellationToken);
            if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                _navigation.NavigateTo("/admin/signin");
            }
            return httpResponseMessage;
        }
    }
}
