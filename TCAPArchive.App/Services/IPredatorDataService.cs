using System.Diagnostics.Metrics;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public interface IPredatorDataService 
	{
        Task<IEnumerable<Predator>> GetAllPredators();
    }
}
