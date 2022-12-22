using Microsoft.Extensions.Hosting;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Api.Models
{
    public interface ITCAPRepository
    {
        Predator AddPredator(Predator predator);
		Decoy AddDecoy(Decoy decoy);
        ChatSession CreateChatSession (ChatSession chatSession);
        int AddChatLines(List<ChatLine> chatlines);
        IEnumerable<Predator> GetAllPredators();
        IEnumerable<ChatSession> GetAllChatSessions();
        IEnumerable<Decoy> GetAllDecoys();
        Predator GetPredatorById(Guid Id);
        Decoy GetDecoyById(Guid Id);
        ChatSession GetChatSessionById(Guid Id);
        void UpdatePredator(Predator predator);
        void UpdateDecoy(Decoy decoy);
        void UpdateChatSession(ChatSession chatsession);
        void DeletePredator(Guid Id);
        void DeleteDecoy(Guid Id);
        void DeleteChatSession(Guid Id);
        List<ChatLine> GetAllChatLinesByChatSession(Guid chatSessionId);
        bool SaveAll();

    }
}
