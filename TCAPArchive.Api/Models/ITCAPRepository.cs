using Microsoft.Extensions.Hosting;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Api.Models
{
    public interface ITCAPRepository
    {
        Predator AddPredator(Predator predator);
		Decoy AddDecoy(Decoy decoy);
        ChatSession CreateChatSession (ChatSession chatSession);
        bool CreateChatlog(List<ChatLine> chatlines);
		IEnumerable<ChatLine> GetAllChatLines();
        IEnumerable<Predator> GetAllPredators();
        IEnumerable<Decoy> GetAllDecoys();
        Predator GetPredatorById(Guid Id);
        Decoy GetDecoyById(Guid Id);
        void UpdatePredator(Predator predator);
        void DeletePredator(Guid Id);
        bool SaveAll();

    }
}
