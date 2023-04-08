using ScriptService.Models.DTO.User;

namespace ScriptService.App.Services
{
				public interface IScriptAccountService
				{
								Task<bool> Login(LoginUserDTO loginUserDTO);

								Task<bool> Logout();

								Task<bool> Register(RegisterUserDTO);
				}
}
