using System.Diagnostics.Metrics;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public interface IPredatorDataService 
	{
        Task<IEnumerable<Predator>> GetAllPredators();
        Task<Predator> GetPredatorById(Guid predatorId);
        Task<Predator> AddPredator(Predator predator);
        Task UpdatePredator(Predator predator);
        Task DeletePredator(Guid Id);
    }
}
