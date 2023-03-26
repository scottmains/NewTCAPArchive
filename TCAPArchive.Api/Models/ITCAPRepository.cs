using Microsoft.AspNetCore.Mvc;
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
        int InsertChatLine(ChatLine chatLine);
        int AddChatLines(List<ChatLine> chatlines);
        IEnumerable<Predator> GetAllPredators();
        IEnumerable<Predator> FilterPredators(string? searchQuery, string? stingLocation);
        IEnumerable<ChatSession> GetAllChatSessions();
        IEnumerable<Decoy> GetAllDecoys();
        Predator GetPredatorById(Guid Id);
        Decoy GetDecoyById(Guid Id);
        ChatSession GetChatSessionById(Guid Id);
        ChatSession GetChatSessionByPredatorId(Guid Id);
        ChatLine GetChatLineById(Guid Id);
        int UpdatePredator(Predator predator);
        int UpdateDecoy(Decoy decoy);
        int UpdateChatSession(ChatSession chatsession);
        int UpdateChatLine(ChatLine chatLine);
        int DeletePredator(Guid Id);
        int DeleteDecoy(Guid Id);
        int DeleteChatSession(Guid Id);
        Task<int> GetTotalChatlines(Guid chatSessionId);
        List<ChatLine> GetAllChatLinesByChatSession(Guid chatSessionId);
        (List<ChatLine> Data, int Total) FilterChatlines(Guid chatSessionId, int page, int pageSize, string? searchQuery, int? position, string? dropdownQuery);
        bool SaveAll();

    }
}
