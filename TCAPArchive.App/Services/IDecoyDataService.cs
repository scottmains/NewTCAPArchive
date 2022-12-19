using System.Diagnostics.Metrics;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public interface IDecoyDataService 
	{
        Task<IEnumerable<Decoy>> GetAllDecoys();
        Task<Decoy> AddDecoy(Decoy predator);
    }
}
