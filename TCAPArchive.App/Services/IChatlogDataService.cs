﻿using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Services
{
	public interface IChatlogDataService
	{
        Task<IEnumerable<ChatSession>> GetAllChatSessions();
        Task<IEnumerable<ChatLine>> GetAllChatLinesByChatSession(Guid chatSessionId);
        Task<ChatSession> GetChatSessionById(Guid chatSessionId);
        Task<ChatSession> AddChatSession(ChatSessionViewModel chatsession);
        Task UpdateChatSession(ChatSession chatsession);
        Task<int> AddChatLines(List<ChatLine> chatlines);
        Task DeleteChatSession(Guid chatSessionId);
    }
}
