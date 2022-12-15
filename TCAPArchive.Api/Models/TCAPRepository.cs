using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Api.Models
{
    public class TCAPRepository : ITCAPRepository
    {
        private readonly TCAPContext _ctx;
        private readonly ILogger<TCAPRepository> _logger;
        public TCAPRepository(TCAPContext ctx, ILogger<TCAPRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }
        public void AddEntity(object model)
        {

            _ctx.Add(model);
        }
        public void DeletePredator(Guid Id)
        {
            Predator predator = _ctx.Predators.Find(Id);
            var chatId = _ctx.ChatLines.Where(x => x.PredatorId == Id).FirstOrDefault();
            var getChat = _ctx.ChatLines.Where(x => x.ChatId == chatId.ChatId).ToList();
            var decoyId = getChat.Where(x => x.DecoyId != null).FirstOrDefault();
            Decoy decoy = _ctx.Decoys.Find(decoyId.DecoyId);
            foreach (var chatLine in getChat)
            {
                _ctx.ChatLines.Remove(chatLine);
            }
            _ctx.Predators.Remove(predator);
            _ctx.Decoys.Remove(decoy);
        }
        public void UpdatePredator(Predator predator)
        {
            var currentPredator = _ctx.Predators.FirstOrDefault(x => x.Id == predator.Id);

            if (predator.FirstName != null)
            { 
                currentPredator.FirstName = predator.FirstName;
            }
            if (predator.MiddleName != null)
            {
                currentPredator.MiddleName = predator.MiddleName;
            }
            if (predator.FirstName != null)
            {
                currentPredator.LastName = predator.LastName;
            }
            if (predator.Handle != null)
            {
                currentPredator.Handle = predator.Handle;
            }
            if (predator.Description != null)
            {
                currentPredator.Description = predator.Description;
            }
            if (predator.StingLocation != null)
            {
                currentPredator.StingLocation = predator.StingLocation;
            }
            if (predator.ImageData != null && predator.ImageData.Length > 0)
            {
                currentPredator.ImageData = predator.ImageData;
            }
            _ctx.SaveChanges();

        }

        public IEnumerable<ChatLine> GetAllChatLines()
        {
            try
            {
                _logger.LogInformation("GetAllChatLines was called");
                return _ctx.ChatLines
                    .OrderBy(c => c.DateCreated)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get queries: {ex}");
                return null;
            }
        }

        public IEnumerable<Predator> GetAllPredators()
        {
            try
            {
                _logger.LogInformation("GetAllPredators was called");
                return _ctx.Predators
                    .OrderBy(c => c.Id)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get queries: {ex}");
                return null;
            }
        }

        public Predator GetPredatorById(Guid Id)
        {
            return _ctx.Predators.Find(Id);
        }

        public Decoy GetDecoyById(Guid Id)
        {
            return _ctx.Decoys.Find(Id);
        }

        public IEnumerable<Decoy> GetAllDecoys()
        {
            try
            {
                _logger.LogInformation("GetAllDecoys was called");
                return _ctx.Decoys
                    .OrderBy(c => c.Id)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get queries: {ex}");
                return null;
            }
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

