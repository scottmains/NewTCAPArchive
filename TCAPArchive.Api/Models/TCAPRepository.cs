﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

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

       public int InsertChatLine(ChatLine chatLine)
        {
            // need to move position of all entries after position number.
            var existingChatLines = _ctx.ChatLines
                                    .Where(e => e.Position >= chatLine.Position &&  
                                           e.ChatSessionId == chatLine.ChatSessionId);

            foreach (var existingChatline in existingChatLines)
            {
                existingChatline.Position++;
            }

            _ctx.ChatLines.Add(chatLine);
            var success = _ctx.SaveChanges();

            return success;
        }

        public int DeletePredator(Guid Id)
        {
            var foundPredator = _ctx.Predators.FirstOrDefault(e => e.Id == Id);
            if (foundPredator == null) return 0;

            _ctx.Remove(foundPredator);
            var success =_ctx.SaveChanges();

            return success;
        }

        public int DeleteDecoy(Guid Id)
        {
            var foundDecoy = _ctx.Decoys.FirstOrDefault(e => e.Id == Id);
            if (foundDecoy == null) return 0;

            _ctx.Remove(foundDecoy);
           var success = _ctx.SaveChanges();

            return success;
        }

        public int DeleteChatSession(Guid Id)
        {
            var foundChatSession = _ctx.ChatSessions.FirstOrDefault(e => e.Id == Id);
            if (foundChatSession == null) return 0 ;

            _ctx.Remove(foundChatSession);
           var success= _ctx.SaveChanges();

            return success;
        }

        public int UpdatePredator(Predator predator)
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
           var success = _ctx.SaveChanges();

            return success;
        }

        public int UpdateDecoy(Decoy decoy)
        {
            var currentDecoy = _ctx.Decoys.FirstOrDefault(x => x.Id == decoy.Id);

            if (decoy != null)
            {
                currentDecoy.Handle = decoy.Handle;
                currentDecoy.ImageTitle = decoy.ImageTitle;
                currentDecoy.ImageData = decoy.ImageData;
                currentDecoy.PredatorId = decoy.PredatorId;
            }

           var success= _ctx.SaveChanges();

            return success;
        }

        public int UpdateChatSession(ChatSession chatsession)
        {
            var currentChatSession = _ctx.ChatSessions.FirstOrDefault(x => x.Id == chatsession.Id);

            if (chatsession != null)
            {
                currentChatSession.Name = chatsession.Name;
                currentChatSession.ChatLength = chatsession.ChatLength;
            }

            var success = _ctx.SaveChanges();

            return success;
        }

        public int UpdateChatLine(ChatLine chatLine)
        {
            var currentChatLine = _ctx.ChatLines.FirstOrDefault(x => x.Id == chatLine.Id);

            if (chatLine != null)
            {
                currentChatLine.Message = chatLine.Message;
                currentChatLine.TimeStamp = chatLine.TimeStamp;
                currentChatLine.SenderHandle = chatLine.SenderHandle;
                currentChatLine.SenderId = chatLine.SenderId;   
            }

            var success = _ctx.SaveChanges();

            return success;
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


        public IEnumerable<Predator> FilterPredators(string? searchQuery, string? stingLocation)
        {
            try
            {
                _logger.LogInformation("FilterPredators was called");
                var filteredPredators = _ctx.Predators
                                        .OrderBy(c => c.Id).ToList();

                if (!string.IsNullOrEmpty(searchQuery))
                {
                 filteredPredators = filteredPredators.Where(x => x.FirstName.ToLower().Contains(searchQuery.ToLower()) || x.LastName.ToLower().Contains(searchQuery.ToLower())).ToList();  
                }

                if (!string.IsNullOrEmpty(stingLocation))
                {
                    filteredPredators = filteredPredators.Where(p =>
                        p.StingLocation.Contains(stingLocation, StringComparison.OrdinalIgnoreCase)).ToList();
                }


                return filteredPredators.ToList();
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

        public List<ChatLine> GetAllChatLinesByChatSession(Guid chatSessionId)
        {
            try
            {
                _logger.LogInformation("GetAllChatLinesByChatSession was called");
                return _ctx.ChatLines
                    .Where(x => x.ChatSessionId == chatSessionId)
                    .OrderBy(c => c.Position)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get queries: {ex}");
                return null;
            }
        }

        public (List<ChatLine> Data, int Total) FilterChatlines(Guid chatSessionId, int page, int pageSize, string? searchQuery, int? position, string? dropdownQuery)
        {
            try
            {
                _logger.LogInformation("GetAllChatLinesByChatSession was called");

                string format = "dd/MM/yyyy";

                var filteredChatLines = _ctx.ChatLines
                    .Where(x => x.ChatSessionId == chatSessionId
                            && (searchQuery == null || x.Message.Contains(searchQuery))
                            && (dropdownQuery == null || x.TimeStamp.Date == DateTime.ParseExact(dropdownQuery, format, CultureInfo.InvariantCulture)));

                if (position.HasValue)
                {
                    filteredChatLines = filteredChatLines.Where(x => x.Position >= position);
                }
                int totalCount = filteredChatLines.Count();
                var chatLines = filteredChatLines
                    .OrderBy(c => c.Position)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return (chatLines, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get queries: {ex}");
                return (null, 0);
            }
        }

        public async Task<int> GetTotalChatlines(Guid chatSessionId)
        {
            int totalChatlines = await _ctx.ChatLines
                .Where(x => x.ChatSessionId == chatSessionId)
                .CountAsync();

            return totalChatlines;
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

        public ChatSession GetChatSessionByPredatorId(Guid Id)
        {
            return _ctx.ChatSessions.Where(x => x.PredatorId == Id).FirstOrDefault();
        }

        public ChatLine GetChatLineById(Guid Id)
        {
            return _ctx.ChatLines.Find(Id);
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

