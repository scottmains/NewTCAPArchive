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
		public Predator AddPredator(Predator predator)
		{
			var addedEntity = _ctx.Predators.Add(predator);
			_ctx.SaveChanges();
			return addedEntity.Entity;
		}

		public Decoy AddDecoy(Decoy decoy)
		{
			var addedEntity = _ctx.Decoys.Add(decoy);
			_ctx.SaveChanges();
			return addedEntity.Entity;
		}

        public ChatSession CreateChatSession (ChatSession chatSession)
        {
            foreach(var chatline in chatSession.Lines)
            {
                _ctx.ChatLines.Add(chatline);
            }

			var addedEntity = _ctx.ChatSessions.Add(chatSession);
			_ctx.SaveChanges();
			return addedEntity.Entity;
		}

        public bool CreateChatlog (List<ChatLine> chatlog)
        {
            foreach(var line in chatlog)
            {
				 _ctx.ChatLines.Add(line);
			}

            var success = _ctx.SaveChanges();

            if (success == chatlog.Count())
                return true;

            return false;
        }

		public void DeletePredator(Guid Id)
        {
  
        }
        public void UpdatePredator(Predator predator)
        {
            var currentPredator = _ctx.Predators.FirstOrDefault(x => x.Id == predator.Id);

            if (predator != null)
            { 
                currentPredator.FirstName = predator.FirstName;
                currentPredator.LastName = predator.LastName;
                currentPredator.Handle = predator.Handle;
                currentPredator.Description = predator.Description;
                currentPredator.StingLocation = predator.StingLocation;
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
                    .OrderBy(c => c.Position)
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

