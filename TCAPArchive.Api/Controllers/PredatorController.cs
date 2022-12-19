using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCAPArchive.Api.Models;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredatorController : ControllerBase
    {

        private readonly ITCAPRepository _repository;


        public PredatorController(ITCAPRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetPredators()
        {
            return Ok(_repository.GetAllPredators());
        }
        [HttpPost]
        public ActionResult CreatePredator([FromBody] Predator predator)
		{

		
			if (predator == null)
				return BadRequest();

			if (predator.FirstName == string.Empty || predator.Handle == string.Empty)
			{
				ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var createdPredator = _repository.AddPredator(predator);

			return Created("predator", createdPredator);
		}

	}
}
