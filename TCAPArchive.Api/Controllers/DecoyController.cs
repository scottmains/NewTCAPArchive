using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{id}")]
        public IActionResult GetDecoyById(Guid id)
        {
            return Ok(_repository.GetDecoyById(id));
        }
        [Authorize]
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
        [Authorize]
        [HttpPut]
        public IActionResult UpdateDecoy([FromBody] Decoy decoy)
        {
            if (decoy == null)
                return BadRequest();

            if (decoy.Handle == string.Empty)
            {
                ModelState.AddModelError("Handle", "The handle shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var decoyToUpdate = _repository.GetDecoyById(decoy.Id);

            if (decoyToUpdate == null)
                return NotFound();

            var success = _repository.UpdateDecoy(decoy);

            return Ok(success); //success
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteDecoy(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var decoyToDelete = _repository.GetDecoyById(id);
            if (decoyToDelete == null)
                return NotFound();

            var success = _repository.DeleteDecoy(id);

            return Ok(success);//success
        }
    }
}
