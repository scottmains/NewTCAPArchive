using System.Diagnostics.Metrics;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.App.Services
{
	public interface IDecoyDataService 
	{
        Task<IEnumerable<Decoy>> GetAllDecoys();
        Task<Decoy> GetDecoyById(Guid decoyId);
        Task<Decoy> AddDecoy(Decoy decoy);
        Task<int> UpdateDecoy(Decoy decoy);
        Task<int> DeleteDecoy(Guid Id);
    }
}
