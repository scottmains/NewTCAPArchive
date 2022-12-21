﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TCAPArchive.App.Components.Admin;
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
			var addedEntity = _ctx.ChatSessions.Add(chatSession);
			_ctx.SaveChanges();
			return addedEntity.Entity;
        }

        public int AddChatLines(List<ChatLine> chatlines)
        {
            if (chatlines != null)
                foreach (var chatline in chatlines)
                {
                    _ctx.ChatLines.Add(chatline);
                }
            var success = _ctx.SaveChanges();

            return success;
        }

    public void DeletePredator(Guid Id)
        {
            var foundPredator = _ctx.Predators.FirstOrDefault(e => e.Id == Id);
            if (foundPredator == null) return;

            _ctx.Remove(foundPredator);
            _ctx.SaveChanges();
        }

        public void DeleteDecoy(Guid Id)
        {
            var foundDecoy = _ctx.Decoys.FirstOrDefault(e => e.Id == Id);
            if (foundDecoy == null) return;

            _ctx.Remove(foundDecoy);
            _ctx.SaveChanges();
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
                currentPredator.ImageTitle = predator.ImageTitle;
                currentPredator.ImageData = predator.ImageData;
            }
            _ctx.SaveChanges();

        }

        public void UpdateDecoy(Decoy decoy)
        {
            var currentDecoy = _ctx.Decoys.FirstOrDefault(x => x.Id == decoy.Id);

            if (decoy != null)
            {
                currentDecoy.Handle = decoy.Handle;
                currentDecoy.ImageTitle = decoy.ImageTitle;
                currentDecoy.ImageData = decoy.ImageData;
                currentDecoy.PredatorId = decoy.PredatorId;
            }

            _ctx.SaveChanges();
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

        public IEnumerable<ChatSession> GetAllChatSessions()
        {
            try
            {
                _logger.LogInformation("GetAllChatSessions was called");
                return _ctx.ChatSessions
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
        public ChatSession GetChatSessionById(Guid Id)
        {
            return _ctx.ChatSessions.Find(Id);
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

