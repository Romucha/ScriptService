using ScriptService.Models.DTO.User;

namespace ScriptService.API.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO loginUserDTO);

        Task<string> CreateToken();
    }
}
