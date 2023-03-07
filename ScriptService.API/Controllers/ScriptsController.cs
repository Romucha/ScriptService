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
				filter = filter.ToLower();
				return Ok(scripts.Where(c => c.Content.ToLower().Contains(filter) 
					|| c.Name.ToLower().Contains(filter)
					|| c.UpdatedAt.ToString().ToLower().Contains(filter)
					|| c.CreatedAt.ToString().ToLower().Contains(filter)));
			}
			return Ok(scripts);
		}

		[HttpGet("{id:int}")]
		public ActionResult GetById(int id)
		{
			var script = _dbContext.Scripts.FirstOrDefault(x => x.Id == id);
			if (script != null)
			{
				return Ok(script);
			}
			return NotFound();
		}

		[HttpPost]
		public ActionResult Post(Script script)
		{
			if (ModelState.IsValid)
			{
				if (script != null)
				{
					_dbContext.Scripts.Add(script);
					_dbContext.SaveChanges();
					return Ok(script);
				}
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
			if (ModelState.IsValid)
			{
				if (script != null)
				{
					var dbscript = _dbContext.Scripts.FirstOrDefault(c => c.Name == script.Name);
					if (dbscript != null)
					{
						dbscript.Content = script.Content;
						dbscript.Type = script.Type;
						dbscript.UpdatedAt = DateTime.UtcNow;
						//broken
						_dbContext.Update(dbscript);
						_dbContext.SaveChanges();
						return Ok(dbscript);
					}
				}
				return NotFound();
			}
			return BadRequest();
		}
	}
}
