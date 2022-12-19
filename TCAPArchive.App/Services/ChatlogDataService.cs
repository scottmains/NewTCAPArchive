using System.Text.Json;
using System.Text;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public class ChatlogDataService : IChatlogDataService
	{

        private readonly HttpClient _httpClient;

        public ChatlogDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ChatSession> AddChatSession(ChatSession chatsession)
        {
            var decoyJson =
                new StringContent(JsonSerializer.Serialize(chatsession), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/chatlog", decoyJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ChatSession>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }
    }
}
