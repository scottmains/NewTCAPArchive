using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TCAPArchive.Api.Models;
using TCAPArchive.Shared.Domain;

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

        [HttpPost]
		public ActionResult CreateChatlog([FromBody]ChatSession chatsession)
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



	}
}
