using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCAPArchive.Api.Models;

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

    }
}
