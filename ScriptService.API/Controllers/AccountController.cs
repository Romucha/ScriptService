using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScriptService.DataManagement;
using ScriptService.Models.DTO;

namespace ScriptService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ScriptUser> _userManager;
		private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<ScriptUser> userManager, ILogger<AccountController> logger)
        {
			_userManager = userManager;
			_logger = logger;
        }

        [HttpPost]
		[Route(nameof(Register))]
		public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
		{
			return Ok();
		}

		[HttpPost]
		[Route(nameof(Login))]
		public async Task<IActionResult> Login()
		{
			return Ok();
		}

		[Authorize]
		[HttpPost]
		[Route(nameof(Logout))]
		public async Task<IActionResult> Logout()
		{
			return Ok();
		}
	}
}
