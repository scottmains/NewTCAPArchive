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
        [HttpGet("chatline/{id}")]
        public IActionResult GetChatLineById(Guid id)
        {
            return Ok(_repository.GetChatLineById(id));
        }

        [HttpGet("getchatlines/{id}")]
        public IActionResult GetChatLinesByChatSession(Guid id)
        {
            return Ok(_repository.GetAllChatLinesByChatSession(id));
        }

        [HttpPost]
		public ActionResult CreateChatSession([FromBody]ChatSession chatsession)
		{
            if (chatsession == null)
                return BadRequest();

            if (chatsession.Id == null || chatsession.PredatorId == null || chatsession.DecoyId == null)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var createdChatSession = _repository.CreateChatSession(chatsession);

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

            var chatSessionToUpdate = _repository.GetChatSessionById(chatsession.Id);

            if (chatSessionToUpdate == null)
                return NotFound();

           var success= _repository.UpdateChatSession(chatsession);

            return Ok(success); //success
        }

        [HttpPut("chatline")]
        public IActionResult UpdateChatLine([FromBody] ChatLine chatLine)
        {
            if (chatLine == null)
                return BadRequest();

            if (chatLine.Id == Guid.Empty)
            {
                ModelState.AddModelError("Id", "No Id");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var chatLineToUpdate = _repository.GetChatLineById(chatLine.Id);

            if (chatLineToUpdate == null)
                return NotFound();

            var success = _repository.UpdateChatLine(chatLine);

            return Ok(success); //success
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

        public ActionResult InsertChatLine([FromBody]ChatLine chatLine)
        {
            if (chatLine == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _repository.InsertChatLine(chatLine);

            return Ok(success);//success
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteChatSession(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var chatSessionToDelete = _repository.GetChatSessionById(id);
            if (chatSessionToDelete == null)
                return NotFound();

            var success =_repository.DeleteChatSession(id);

            return Ok(success);//success
        }
    }
}
