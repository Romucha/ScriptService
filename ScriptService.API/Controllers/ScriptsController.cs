using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog.Filters;
using ScriptService.DataManagement;
using ScriptService.DataManagement.Repository;
using ScriptService.Models.Data;
using ScriptService.Models.DTO.Script;
using ScriptService.Models.Paging;
using System.Xml.Linq;

namespace ScriptService.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/{controller}")]
    public class ScriptsController : Controller
    {
        private readonly ILogger<ScriptsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScriptsController(ILogger<ScriptsController> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery]RequestParams requestParams, string? filter = null)
								{
												_logger.LogInformation($"{this.HttpContext.User?.Identity?.Name} attempts to get a set of scripts");
												var scripts = await _unitOfWork.Scripts.GetAll(requestParams, x => string.IsNullOrEmpty(filter) ? 
            true 
            : 
            x.Content.ToLower().IndexOf(filter.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0
            || x.Name.ToLower().IndexOf(filter.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0
            || x.UpdatedAt.ToString().IndexOf(filter.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0
            || x.CreatedAt.ToString().IndexOf(filter.ToLower(), StringComparison.OrdinalIgnoreCase) >= 0);
            var result = _mapper.Map<IList<GetScriptDTO>>(scripts);
            return Ok(result);
        }

        [HttpGet("{id:int}", Name = nameof(GetById))]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
												_logger.LogInformation($"{this.HttpContext.User?.Identity?.Name} attempts to get set the script with id: {id}");
												var script = await _unitOfWork.Scripts.Get(x => x.Id == id);
            var result = _mapper.Map<DetailScriptDTO>(script);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateScriptDTO createScriptDTO)
        {
            if (ModelState.IsValid)
            {
																_logger.LogInformation($"{this.HttpContext.User?.Identity?.Name} attempts to add a new script");
																var script = _mapper.Map<Script>(createScriptDTO);
                script.CreatedAt = DateTime.UtcNow;
                script.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Scripts.Insert(script);
                await _unitOfWork.Save();
                return CreatedAtRoute(nameof(GetById), new { id = script.Id }, script);
            }
            else
            {
                _logger.LogError($"Invalid post attempt in {nameof(Post)}");
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid && id > 0) 
            {
																_logger.LogInformation($"{this.HttpContext.User?.Identity?.Name} attempts to add delete the script with id: {id}");
																var script = await _unitOfWork.Scripts.Get(x => x.Id == id);
                if (script == null)
                {
                    _logger.LogError($"Script with id {id} does not exist");
                    return NotFound(id);
                }
                else
                {
                    await _unitOfWork.Scripts.Delete(id);
                    await _unitOfWork.Save();

                    return Accepted();
                }
            }
            else
            {
                _logger.LogError($"Invalid delete attempt in {nameof(Delete)}");
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody]UpdateScriptDTO updateScriptDTO)
        {
            if (ModelState.IsValid && id > 0)
            {
																_logger.LogInformation($"{this.HttpContext.User?.Identity?.Name} attempts to add change the script with id: {id}");
																var script = await _unitOfWork.Scripts.Get(x => x.Id == id);
                if (script == null)
                {
                    _logger.LogError($"script with id {id} does not exist");
                    return NotFound(id);
                }
                else
                {
                    _mapper.Map(updateScriptDTO, script);
                    script.UpdatedAt = DateTime.UtcNow;
                    _unitOfWork.Scripts.Update(script);
                    await _unitOfWork.Save();

                    return Accepted();
                }
            }
            else
            {
                _logger.LogError($"Invalid put attempt in {nameof(Put)}");
                return BadRequest(ModelState);
            }
        }
    }
}
