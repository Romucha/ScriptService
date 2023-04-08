using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ScriptService.Models.DTO.User;
using System.Net.Http.Json;

namespace ScriptService.App.Services
{
				public class ScriptAccountService : IScriptAccountService
				{
								private HttpClient _httpClient;
								private ILocalStorageService _localStorageService;

        public ScriptAccountService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService)
        {
            _httpClient	= httpClient;
												_localStorageService = localStorageService;
        }

        public async Task<bool> Login(LoginUserDTO loginUserDTO)
								{
												var response = await _httpClient.PostAsJsonAsync<LoginUserDTO>("/api/account/login", loginUserDTO);
												if (response.IsSuccessStatusCode)
												{
																await _localStorageService.SetItemAsStringAsync("jwttoken", await response.Content.ReadAsStringAsync());
																return true;
												}
												return false;
								}

								public Task<bool> Logout()
								{
												throw new NotImplementedException();
								}

								public Task<bool> Register(RegisterUserDTO registerUserDTO)
								{
												throw new NotImplementedException();
								}
				}
}
