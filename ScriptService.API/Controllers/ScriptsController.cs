using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ScriptService.API.Controllers
{
	[ApiController]
	[Route("/api/{controller}")]
	public class ScriptsController : Controller
	{
		private readonly ILogger<ScriptsController> _logger;

		public ScriptsController(ILogger<ScriptsController> logger) 
		{
			_logger = logger;
		}

		[HttpGet]
		public ActionResult Get(string? filter = null)
		{
			return Ok();
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
