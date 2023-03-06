using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScriptService.DataManagement;
using ScriptService.Models;

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
			var scripts = _dbContext.Scripts;
			if (filter != null)
			{
				return Ok(scripts.Where(c => c.GetType()
				.GetProperties()
				.Any(x => x.GetValue(c, null).ToString().ToLower().Contains(filter.ToLower()))));
			}
			return Ok(scripts);
		}

		[HttpPost]
		public ActionResult Post(Script script)
		{
			if (script != null)
			{
				_dbContext.Scripts.Add(script);
				_dbContext.SaveChanges();
				return Ok(script);
			}
			return BadRequest();
		}

		[HttpDelete]
		public ActionResult Delete(int? id)
		{
			var script = _dbContext.Scripts.FirstOrDefault(c=>c.Id == id);
			if (script != null)
			{
				_dbContext.Scripts.Remove(script);
				_dbContext.SaveChanges();
				return Ok(script);
			}
			return NotFound();
		}

		[HttpPut]
		public ActionResult Put(Script script)
		{
			if (script != null)
			{
				var dbscript = _dbContext.Scripts.FirstOrDefault(c => c.Id == script.Id);
				if (dbscript != null)
				{
					//broken
					_dbContext.Update(script);
					_dbContext.SaveChanges();
					return Ok(script);
				}
			}
			return NotFound();
		}
	}
}
