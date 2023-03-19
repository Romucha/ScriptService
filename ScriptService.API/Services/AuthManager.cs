using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ScriptService.Models.Data;
using ScriptService.Models.DTO.User;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ScriptService.API.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ScriptUser> _userManager;
        private readonly IConfiguration _configuration;
        private ScriptUser _user;

        public AuthManager(UserManager<ScriptUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            /*
             * 1. Get signing credentials from secret key.
             * 2. Get claims for user.
             * 3. Get token options.
             * 4. Generate token.
             */
            var credentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(credentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _configuration.GetSection("Jwt").GetValue<string>("IssuerSigningKey");
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IList<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            foreach (var role in await _userManager.GetRolesAsync(_user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IList<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var expiration = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings.GetSection("Lifetime").Value));
            var jwtToken = new JwtSecurityToken(issuer: jwtSettings.GetSection("ValidIssuer").Value,
                audience: jwtSettings.GetSection("ValidAudience").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials);

            return jwtToken;
        }

        public async Task<bool> ValidateUser(LoginUserDTO loginUserDTO)
        {
            _user = await _userManager.FindByNameAsync(loginUserDTO.Email);
            return _user != null && await _userManager.CheckPasswordAsync(_user, loginUserDTO.Password);
        }
    }
}
