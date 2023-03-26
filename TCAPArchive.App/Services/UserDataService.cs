using System.Net.Http;
using System.Text.Json;
using System.Text;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HttpClient _httpClient;

        public UserDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

        public async Task<bool> LoginUserAsync(LoginViewModel user)
        {
            var userJson =
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Account/login", userJson);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> LogoutUserAsync()
        {
            var response = await _httpClient.PostAsync("/api/Account/logout", null);
            return response.IsSuccessStatusCode;
        }
    }
}
