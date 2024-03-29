﻿using System.Text.Json;
using System.Text;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace TCAPArchive.App.Services
{
	public class ChatlogDataService : IChatlogDataService
	{

        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        public ChatlogDataService(HttpClient httpClient, ILocalStorageService localStorageService)
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

        public async Task<ChatSession> GetChatSessionByPredatorId(Guid predatorId)
        {
            return await JsonSerializer.DeserializeAsync<ChatSession>
                (await _httpClient.GetStreamAsync($"api/chatlog/predatorid/{predatorId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<ChatLine> GetChatLineById(Guid chatLineId)
        {
            return await JsonSerializer.DeserializeAsync<ChatLine>
                (await _httpClient.GetStreamAsync($"api/chatlog/chatline/{chatLineId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<int> UpdateChatSession(ChatSession chatsession)
        {
            await SetAuthorizationHeaderAsync();
            var chatSessionJson =
                new StringContent(JsonSerializer.Serialize(chatsession), Encoding.UTF8, "application/json");

           var response = await _httpClient.PutAsync("api/chatlog", chatSessionJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;
        }

        public async Task<int> UpdateChatLine(ChatLine chatLine)
        {
            await SetAuthorizationHeaderAsync();
            var chatLineJson =
                new StringContent(JsonSerializer.Serialize(chatLine), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/chatlog/chatline", chatLineJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;
        }

        public async Task<ChatSession> AddChatSession(ChatSession chatsession)
        {
            await SetAuthorizationHeaderAsync();
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
            await SetAuthorizationHeaderAsync();
            var chatLinesJson =
                new StringContent(JsonSerializer.Serialize(chatlines), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/chatlog/addchatlines", chatLinesJson);

            if (response.IsSuccessStatusCode)
            {
                return chatlines.Count();
            }

            return 0;
        }

        public async Task<int> InsertChatLine(ChatLine chatLine)
        {
            await SetAuthorizationHeaderAsync();
            var chatLinesJson =
               new StringContent(JsonSerializer.Serialize(chatLine), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/chatlog/insertchatline", chatLinesJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }

            return 0;

        }

        public async Task<int> DeleteChatSession(Guid chatSessionId)
        {
            await SetAuthorizationHeaderAsync();
            var response =  await _httpClient.DeleteAsync($"api/chatlog/{chatSessionId}");
            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<int>(await response.Content.ReadAsStreamAsync());
            }
            return 0;
        }
    }
}
