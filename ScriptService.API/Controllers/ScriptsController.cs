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
		public async Task<IActionResult> Get(string? filter = null)
		{
			var scripts = await _dbContext.Scripts.ToListAsync();
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
		public async Task<IActionResult> GetById(int id)
		{
			var script = await _dbContext.Scripts.FirstOrDefaultAsync(x => x.Id == id);
			if (script != null)
			{
				return Ok(script);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Post(Script script)
		{
			if (ModelState.IsValid)
			{
				if (script != null)
				{
					await _dbContext.Scripts.AddAsync(script);
					_dbContext.SaveChanges();
					return Ok(script);
				}
			}
			return BadRequest();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int? id)
		{
			var script = await _dbContext.Scripts.FirstOrDefaultAsync(c => c.Id == id);
			if (script != null)
			{
				_dbContext.Scripts.Remove(script);
				_dbContext.SaveChanges();
				return Ok(script);
			}
			return NotFound();
		}

		[HttpPut]
		public async Task<IActionResult> Put(Script script)
		{
			if (ModelState.IsValid)
			{
				if (script != null)
				{
					var dbscript = await _dbContext.Scripts.FirstOrDefaultAsync(c => c.Name == script.Name);
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
