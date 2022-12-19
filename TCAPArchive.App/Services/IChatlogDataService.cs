using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public interface IChatlogDataService
	{

        Task<ChatSession> AddChatSession(ChatSession chatsession);

    }
}
