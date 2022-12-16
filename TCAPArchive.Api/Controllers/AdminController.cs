using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCAPArchive.Api.Models;
using TCAPArchive.App.Pages;
using TCAPArchive.Shared.Domain;

namespace TCAPArchive.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly ITCAPRepository _repository;

		public AdminController(ITCAPRepository repository) {
		_repository= repository;

		}

	

	}


}
