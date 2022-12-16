using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCAPArchive.Api.Models;

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

		public ActionResult CreateChatlog(String Chatlog)
		{

			return Ok();
		}

	}
}
