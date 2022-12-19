using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCAPArchive.Api.Models;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DecoyController : ControllerBase
	{

		private readonly ITCAPRepository _repository;


		public DecoyController(ITCAPRepository repository)
		{
			_repository = repository;
		}

        [HttpGet]
        public IActionResult GetDecoys()
        {
            return Ok(_repository.GetAllDecoys());
        }
        [HttpPost]
		public ActionResult CreateDecoy([FromBody] Decoy decoy)
		{
			
			if (decoy == null)
				return BadRequest();

			if (decoy.Handle == string.Empty)
			{
				ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var createdDecoy = _repository.AddDecoy(decoy);

			return Created("decoy", createdDecoy);

		}
	}
}
