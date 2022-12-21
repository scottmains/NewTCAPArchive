using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public interface IChatlogDataService
	{
        Task<IEnumerable<ChatSession>> GetAllChatSessions();
        Task<ChatSession> AddChatSession(ChatSession chatsession);

    }
}
