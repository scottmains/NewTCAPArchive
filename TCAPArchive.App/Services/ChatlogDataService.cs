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
        public async Task<IEnumerable<ChatSession>> GetAllChatSessions()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<ChatSession>>
                (await _httpClient.GetStreamAsync($"api/chatlog"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ChatSession> GetChatSessionById(Guid chatSessionId)
        {
            return await JsonSerializer.DeserializeAsync<ChatSession>
                (await _httpClient.GetStreamAsync($"api/chatlog/{chatSessionId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ChatSession> AddChatSession(ChatSession chatsession)
        {
            var chatSessionJson =
                new StringContent(JsonSerializer.Serialize(chatsession), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/chatlog", chatSessionJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<ChatSession>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task<int> AddChatLines(List<ChatLine> chatlines )
        {
            var chatLinesJson =
                new StringContent(JsonSerializer.Serialize(chatlines), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/chatlog/addchatlines", chatLinesJson);

            if (response.IsSuccessStatusCode)
            {
                return chatlines.Count();
            }

            return 0;
        }
    }
}
