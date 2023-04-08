using Microsoft.AspNetCore.Components.Authorization;
using ScriptService.Models.Data;
using ScriptService.Models.DTO.User;
using System.Security.Claims;

namespace ScriptService.App.Services
{
				public class ScriptAuthenticationStateProvider : AuthenticationStateProvider
				{
								private IScriptAccountService _accountService;
								private ScriptUser _currentUser;

        public ScriptAuthenticationStateProvider(IScriptAccountService accountService)
        {
												_accountService = accountService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
								{
												var identity = new ClaimsIdentity();
												try
												{
																var userInfo = await GetCurrentUser();
																if (userInfo.IsAuthenticated)
																{
																				var claims = new[] { new Claim(ClaimTypes.Name, _currentUser.UserName) }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
																				identity = new ClaimsIdentity(claims, "Server authentication");
																}
												}
												catch (HttpRequestException ex)
												{
																Console.WriteLine("Request failed:" + ex.ToString());
												}
												return new AuthenticationState(new ClaimsPrincipal(identity));
								}

								private async Task<ScriptUser> GetCurrentUser()
								{
												if (_currentUser != null && _currentUser.IsAuthenticated) return _currentUser;
												_currentUser = await _accountService.CurrentUserInfo();
												return _currentUser;
								}
								public async Task Logout()
								{
												await _accountService.Logout();
												_currentUser = null;
												NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
								}
								public async Task Login(LoginUserDTO loginParameters)
								{
												await _accountService.Login(loginParameters);
												NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
								}
								public async Task Register(RegisterUserDTO registerParameters)
								{
												await _accountService.Register(registerParameters);
												NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
								}
				}
}
