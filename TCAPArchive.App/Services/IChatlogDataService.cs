using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.App.Services
{
	public interface IChatlogDataService
	{
        Task<IEnumerable<ChatSession>> GetAllChatSessions();
        Task<IEnumerable<ChatLine>> GetAllChatLinesByChatSession(Guid chatSessionId);
        Task<ChatSession> GetChatSessionById(Guid chatSessionId);
        Task<ChatLine> GetChatLineById(Guid chatLineId);
        Task<ChatSession> AddChatSession(ChatSession chatsession);
        Task<int> UpdateChatSession(ChatSession chatsession);
        Task<int> UpdateChatLine(ChatLine chatLine);
        Task<int> AddChatLines(List<ChatLine> chatlines);
        Task<int> InsertChatLine(ChatLine chatLine);
        Task<int> DeleteChatSession(Guid chatSessionId);
    }
}
