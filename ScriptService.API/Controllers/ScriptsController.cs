using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScriptService.DataManagement;

namespace ScriptService.API.Controllers
{
	[ApiController]
	[Route("/api/{controller}")]
	public class ScriptsController : Controller
	{
		private readonly ILogger<ScriptsController> _logger;

		private readonly ScriptDbContext _dbContext;

		public ScriptsController(ILogger<ScriptsController> logger, ScriptDbContext scriptDbContext) 
		{
			_logger = logger;
			_dbContext = scriptDbContext;
		}

		[HttpGet]
		public ActionResult Get(string? filter = null)
		{
			return Ok(_dbContext.Scripts);
		}

		[HttpPost]
		[Authorize]
		public ActionResult Post(string? filter = null)
		{
			return Ok();
		}

		[HttpDelete]
		[Authorize]
		public ActionResult Delete(string? filter = null)
		{
			return Ok();
		}

		[HttpPut]
		[Authorize]
		public ActionResult Put(string? filter = null)
		{
			return Ok();
		}
	}
}
