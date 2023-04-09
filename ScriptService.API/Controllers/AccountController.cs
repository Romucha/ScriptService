using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScriptService.API.Services;
using ScriptService.Models.Data;
using ScriptService.Models.DTO;
using ScriptService.Models.DTO.User;

namespace ScriptService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ScriptUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ScriptUser> userManager, ILogger<AccountController> logger, IAuthManager authManager, IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _authManager = authManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO userDTO)
        {
            _logger.LogInformation($"Registration attemp for user \'{userDTO.Email}\'");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = _mapper.Map<ScriptUser>(userDTO);
                user.UserName = user.Email;

                var result = await _userManager.CreateAsync(user, userDTO.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRolesAsync(user, userDTO.Roles);

                return Accepted();
            }
            catch (Exception ex)
            {
                var error = $"Error in {nameof(Register)}";
                _logger.LogError(ex, error);
                return Problem(error);
            }
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login attemp for user \'{userDTO.Email}\'");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!await _authManager.ValidateUser(userDTO))
                {
                    return Unauthorized();
                }
                return Accepted(new { Token = await _authManager.CreateToken() });
            }
            catch (Exception ex)
            {
                var error = $"Error in {nameof(Login)}";
                _logger.LogError(ex, error);
                return Problem(error);
            }
        }
    }
}
