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
												_currentUser = new ScriptUser()
												{
																IsAuthenticated = false
												};
								}

								public override async Task<AuthenticationState> GetAuthenticationStateAsync()
								{
												var identity = new ClaimsIdentity();
												var userInfo = _currentUser;
												if (userInfo != null && userInfo.IsAuthenticated)
												{
																var claims = new[] { new Claim(ClaimTypes.Name, _currentUser.UserName) }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
																identity = new ClaimsIdentity(claims, "Server authentication");
												}
												return  await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
								}

								public async Task Logout()
								{
												await _accountService.Logout();
												_currentUser = null;
												NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
								}
								public async Task Login(LoginUserDTO loginParameters)
								{
												if (await _accountService.Login(loginParameters))
												{
																_currentUser = new ScriptUser()
																{
																				UserName = loginParameters.Email,
																				Email = loginParameters.Email,
																				IsAuthenticated = true,
																				// do something with claims here
																};
																NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
												}
								}
								public async Task Register(RegisterUserDTO registerParameters)
								{
												if (await _accountService.Register(registerParameters))
												{
																_currentUser = new ScriptUser()
																{
																				UserName = registerParameters.Email,
																				Email = registerParameters.Email,
																				IsAuthenticated = true,
																				// do something with claims here
																};
																NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
												}
								}
				}
}
