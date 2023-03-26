using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private NavigationManager _navigationManager;
        public CustomAuthenticationStateProvider(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7039/");
            _navigationManager = navigationManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var response = await _httpClient.GetAsync("api/Account/getcurrentuser");

            if (response.IsSuccessStatusCode && response.Content.Headers.ContentType.MediaType == "application/json")
            {
                var user = await response.Content.ReadFromJsonAsync<ApplicationUser>();

                if (user != null)
                {
                    var claims = new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
                };

                    var identity = new ClaimsIdentity(claims, "apiauth");
                    var principal = new ClaimsPrincipal(identity);
                    return new AuthenticationState(principal);
                }
            }
            _navigationManager.NavigateTo("/");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }


    }
}