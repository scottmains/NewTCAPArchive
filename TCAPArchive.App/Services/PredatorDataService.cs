using Blazored.LocalStorage;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public class PredatorDataService : IPredatorDataService
	{
		private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

		public PredatorDataService(HttpClient httpClient, ILocalStorageService localStorageService)
		{
			_httpClient = httpClient;
            _localStorageService = localStorageService;
		}

        private async Task SetAuthorizationHeaderAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
#nullable disable
        public async Task<IEnumerable<Predator>> GetAllPredators()
		{
            return await JsonSerializer.DeserializeAsync<IEnumerable<Predator>>
                (await _httpClient.GetStreamAsync($"api/predator"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Predator> GetPredatorById(Guid predatorId)
        {
            return await JsonSerializer.DeserializeAsync<Predator>
                (await _httpClient.GetStreamAsync($"api/predator/{predatorId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Predator> AddPredator(Predator predator)
        {

            await SetAuthorizationHeaderAsync();
            var predatorJson =
                new StringContent(JsonSerializer.Serialize(predator), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/predator", predatorJson);
            Console.WriteLine($"Status Code: {response.StatusCode}, Message: {response.ReasonPhrase}");
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Predator>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<int> UpdatePredator(Predator predator)
        {
            await SetAuthorizationHeaderAsync();
            var predatorJson =
                new StringContent(JsonSerializer.Serialize(predator), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/predator", predatorJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }
            return 0;
        }

        public async Task<int> DeletePredator(Guid predatorId)
        {
            await SetAuthorizationHeaderAsync();
            var response = await _httpClient.DeleteAsync($"api/predator/{predatorId}");

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;
        }
    }
}
