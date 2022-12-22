using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TCAPArchive.Api.Models;
using TCAPArchive.App.Components.Admin;
using TCAPArchive.Shared.Domain;
using TCAPArchive.Shared.ViewModels;

namespace TCAPArchive.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChatlogController : ControllerBase
	{
		private readonly ITCAPRepository _repository;


		public ChatlogController(ITCAPRepository repository)
		{
			_repository = repository;
		}

        [HttpGet]
        public IActionResult GetChatSessions()
        {
            return Ok(_repository.GetAllChatSessions());
        }

        [HttpGet("{id}")]
        public IActionResult GetChatSessionById(Guid id)
        {
            return Ok(_repository.GetChatSessionById(id));
        }

        [HttpGet("getchatlines/{id}")]
        public IActionResult GetChatLinesByChatSession(Guid id)
        {
            return Ok(_repository.GetAllChatLinesByChatSession(id));
        }

        [HttpPost]
		public ActionResult CreateChatSession([FromBody]ChatSessionViewModel chatsession)
		{
            if (chatsession == null)
                return BadRequest();

            if (chatsession.Id == null || chatsession.PredatorId == null || chatsession.DecoyId == null)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newChat = new ChatSession
            {
                Id = chatsession.Id,
                ChatLength = chatsession.ChatLength,
                PredatorId = chatsession.PredatorId,
                DecoyId = chatsession.DecoyId,
                Name = chatsession.Name,
                Rating = chatsession.Rating,
            };

            var createdChatSession = _repository.CreateChatSession(newChat);

            return Created("chatlog", createdChatSession);
        }

        [HttpPut]
        public IActionResult UpdateChatSession([FromBody] ChatSession chatsession)
        {
            if (chatsession == null)
                return BadRequest();

            if (chatsession.Id == Guid.Empty)
            {
                ModelState.AddModelError("Id", "No Id");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var chatSessionToUpdate = _repository.GetDecoyById(chatsession.Id);

            if (chatSessionToUpdate == null)
                return NotFound();

            _repository.UpdateChatSession(chatsession);

            return NoContent(); //success
        }

        [HttpPost("addchatlines")]
        public ActionResult CreateChatLines([FromBody] List<ChatLine> chatlines)
        {
            if (chatlines == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var chatLines = _repository.AddChatLines(chatlines);

            return Created("chatlog", chatLines);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteChatSession(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var chatSessionToDelete = _repository.GetChatSessionById(id);
            if (chatSessionToDelete == null)
                return NotFound();

            _repository.DeleteChatSession(id);

            return NoContent();//success
        }
    }
}
