using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public class DecoyDataService : IDecoyDataService
	{
		private readonly HttpClient _httpClient;

		public DecoyDataService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		#nullable disable
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
            var decoyJson =
                new StringContent(JsonSerializer.Serialize(decoy), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/decoy", decoyJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Decoy>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateDecoy(Decoy decoy)
        {
            var decoyJson =
                new StringContent(JsonSerializer.Serialize(decoy), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/decoy", decoyJson);
        }

        public async Task DeleteDecoy(Guid decoyId)
        {
            await _httpClient.DeleteAsync($"api/decoy/{decoyId}");
        }

    }
}
