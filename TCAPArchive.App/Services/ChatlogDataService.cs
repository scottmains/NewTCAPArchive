using System.Text.Json;
using System.Text;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

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

        public async Task<IEnumerable<ChatLine>> GetAllChatLinesByChatSession(Guid ChatSessionId)
        {
            return await JsonSerializer.DeserializeAsync<List<ChatLine>>
                (await _httpClient.GetStreamAsync($"api/chatlog/getchatlines/{ChatSessionId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ChatSession> GetChatSessionById(Guid chatSessionId)
        {
            return await JsonSerializer.DeserializeAsync<ChatSession>
                (await _httpClient.GetStreamAsync($"api/chatlog/{chatSessionId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateChatSession(ChatSession chatsession)
        {
            var chatSessionJson =
                new StringContent(JsonSerializer.Serialize(chatsession), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/predator", chatSessionJson);
        }

        public async Task<ChatSession> AddChatSession(ChatSessionViewModel chatsession)
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

        public async Task DeleteChatSession(Guid chatSessionId)
        {
            await _httpClient.DeleteAsync($"api/chatlog/{chatSessionId}");
        }
    }
}
