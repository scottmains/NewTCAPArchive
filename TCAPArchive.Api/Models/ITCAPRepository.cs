using Microsoft.Extensions.Hosting;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Api.Models
{
    public interface ITCAPRepository
    {
        void AddEntity(object model);
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
