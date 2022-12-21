using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public interface IChatlogDataService
	{
        Task<IEnumerable<ChatSession>> GetAllChatSessions();
        Task<ChatSession> GetChatSessionById(Guid chatSessionId);
        Task<ChatSession> AddChatSession(ChatSession chatsession);
        Task<int> AddChatLines(List<ChatLine> chatlines);

    }
}
