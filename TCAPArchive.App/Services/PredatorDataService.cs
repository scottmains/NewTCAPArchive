using System.Diagnostics.Metrics;
using System.Text.Json;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public class PredatorDataService : IPredatorDataService
	{
		private readonly HttpClient _httpClient;

		public PredatorDataService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

        public async Task<IEnumerable<Predator>> GetAllPredators()
		{
            return await JsonSerializer.DeserializeAsync<IEnumerable<Predator>>
                (await _httpClient.GetStreamAsync($"api/predator"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

    }
}
