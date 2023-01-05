using Microsoft.Extensions.Hosting;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.Api.Models
{
    public interface ITCAPRepository
    {
        Predator AddPredator(Predator predator);
		Decoy AddDecoy(Decoy decoy);
        ChatSession CreateChatSession (ChatSession chatSession);
        int InsertChatLine(AdminInsertChatLineViewModel chatLine);
        int AddChatLines(List<ChatLine> chatlines);
        IEnumerable<Predator> GetAllPredators();
        IEnumerable<ChatSession> GetAllChatSessions();
        IEnumerable<Decoy> GetAllDecoys();
        Predator GetPredatorById(Guid Id);
        Decoy GetDecoyById(Guid Id);
        ChatSession GetChatSessionById(Guid Id);
        ChatLine GetChatLineById(Guid Id);
        int UpdatePredator(Predator predator);
        int UpdateDecoy(Decoy decoy);
        int UpdateChatSession(ChatSession chatsession);
        int UpdateChatLine(ChatLine chatLine);
        int DeletePredator(Guid Id);
        int DeleteDecoy(Guid Id);
        int DeleteChatSession(Guid Id);
        List<ChatLine> GetAllChatLinesByChatSession(Guid chatSessionId);
        bool SaveAll();

    }
}
