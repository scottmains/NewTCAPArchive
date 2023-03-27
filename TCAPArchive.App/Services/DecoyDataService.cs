using Blazored.LocalStorage;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public class DecoyDataService : IDecoyDataService
	{
		private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        public DecoyDataService(HttpClient httpClient, ILocalStorageService localStorageService)
		{
			_httpClient = httpClient;
            _localStorageService = localStorageService;
        }
#nullable disable

        private async Task SetAuthorizationHeaderAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            }
        }
        public async Task<IEnumerable<Decoy>> GetAllDecoys()
		{
            return await JsonSerializer.DeserializeAsync<IEnumerable<Decoy>>
                (await _httpClient.GetStreamAsync($"api/decoy"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }


        public async Task<Decoy> GetDecoyById(Guid decoyId)
        {
            return await JsonSerializer.DeserializeAsync<Decoy>
                (await _httpClient.GetStreamAsync($"api/decoy/{decoyId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Decoy> AddDecoy(Decoy decoy)
        {
            await SetAuthorizationHeaderAsync();

            var decoyJson =
                new StringContent(JsonSerializer.Serialize(decoy), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/decoy", decoyJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Decoy>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<int> UpdateDecoy(Decoy decoy)
        {
            await SetAuthorizationHeaderAsync();
            var decoyJson =
                new StringContent(JsonSerializer.Serialize(decoy), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/decoy", decoyJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }
            return 0;
        }

        public async Task<int> DeleteDecoy(Guid decoyId)
        {
            await SetAuthorizationHeaderAsync();
            var response = await _httpClient.DeleteAsync($"api/decoy/{decoyId}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }
            return 0;
        }

    }
}
