using System.Net.Http;
using System.Text.Json;
using System.Text;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace TCAPArchive.App.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public UserDataService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<ApplicationUser> CreateUserAsync(RegisterViewModel user)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Account/register", userJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ApplicationUser>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/account/login", model);

            if (response.IsSuccessStatusCode)
            {
                // Set the authentication cookie in the browser
                await _authenticationStateProvider.GetAuthenticationStateAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            var response = await _httpClient.PostAsync("api/account/logout", null);

            if (response.IsSuccessStatusCode)
            {
                // Remove the authentication cookie from the browser
                await _authenticationStateProvider.GetAuthenticationStateAsync();
            }
        }
    }
}
