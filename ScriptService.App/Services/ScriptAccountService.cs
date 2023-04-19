using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ScriptService.Models.DTO.User;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text.Json;

namespace ScriptService.App.Services
{
    public class ScriptAccountService : IScriptAccountService
    {
        private HttpClient _httpClient;
        private ILocalStorageService _localStorageService;

        public ScriptAccountService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        public async Task<bool> Login(LoginUserDTO loginUserDTO)
        {
            var jsonContent = JsonContent.Create(loginUserDTO);
            var response = await _httpClient.PostAsync($"/api/Account/Login", jsonContent);
            if (response.IsSuccessStatusCode)
            {
																SetToken(response);
																return true;
            }
            return false;
        }

        public async Task<bool> Logout()
        {
            await _localStorageService.SetItemAsStringAsync("jwttoken", string.Empty);
            return true;
        }

        public async Task<bool> Register(RegisterUserDTO registerUserDTO)
        {
            var jsonContent = JsonContent.Create(registerUserDTO);
            var response = await _httpClient.PostAsync("/api/Account/Register", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                SetToken(response);
																return true;
												}
            return false;
        }

        private async void SetToken(HttpResponseMessage? response)
        {
												var jwtoken = await JsonSerializer.DeserializeAsync<JWtToken>(response.Content.ReadAsStream());
												await _localStorageService.SetItemAsStringAsync("jwttoken", jwtoken.token);
								}

        private class JWtToken
        {
            public string token { get; set; }

            public static void SetToken(string inputTOken)
            {

            }
        }
    }
}
