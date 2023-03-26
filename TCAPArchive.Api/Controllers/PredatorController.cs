using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public IActionResult GetPredators(string? searchQuery, string? stingLocation)
        {
            if(searchQuery != null || stingLocation != null)
            {
                return Ok(_repository.FilterPredators(searchQuery, stingLocation));
            }

            return Ok(_repository.GetAllPredators());
        }

        [HttpGet("stinglocations")]
        public IActionResult GetStingLocations(string? searchQuery, string? stingLocation)
        {
            var predators = _repository.GetAllPredators();
            var stingLocations = predators.Select(p => p.StingLocation).Distinct().ToList();
            return Ok(stingLocations);
        }

        [HttpGet("{id}")]
        public IActionResult GetPredatorById(Guid id)
        {
            return Ok(_repository.GetPredatorById(id));
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

        [HttpPut]
        public IActionResult UpdatePredator([FromBody]Predator predator)
        {
            if (predator == null)
                return BadRequest();

            if (predator.FirstName == string.Empty || predator.LastName == string.Empty)
            {
                ModelState.AddModelError("Name/FirstName", "The name or first name shouldn't be empty");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var predatorToUpdate = _repository.GetPredatorById(predator.Id);

            if (predatorToUpdate == null)
                return NotFound();

            var result = _repository.UpdatePredator(predator);

            return Ok(result); //success
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePredator(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var predatorToDelete = _repository.GetPredatorById(id);
            if (predatorToDelete == null)
                return NotFound();

            var success =_repository.DeletePredator(id);

            return Ok(success);//success
        }

     

    }
}
