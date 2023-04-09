using Microsoft.AspNetCore.Components.Authorization;
using ScriptService.Models.Data;
using ScriptService.Models.DTO.User;
using System.Security.Claims;

namespace ScriptService.App.Services
{
    public class ScriptAuthenticationStateProvider : AuthenticationStateProvider
    {
        private IScriptAccountService _accountService;
        public AuthenticatedUserDTO CurrentUser { get; private set; }

        public ScriptAuthenticationStateProvider(IScriptAccountService accountService)
        {
            _accountService = accountService;
            CurrentUser = new AuthenticatedUserDTO()
            {
                IsAuthenticated = false
            };
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var userInfo = CurrentUser;
            if (userInfo != null && userInfo.IsAuthenticated)
            {
                identity = new ClaimsIdentity(new List<Claim>(), "Server authentication");
            }
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        public async Task Logout()
        {
            await _accountService.Logout();
            CurrentUser = default!;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        public async Task Login(LoginUserDTO loginParameters)
        {
            if (await _accountService.Login(loginParameters))
            {
                CurrentUser = new AuthenticatedUserDTO()
                {
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
                CurrentUser = new AuthenticatedUserDTO()
                {
                    Email = registerParameters.Email,
                    IsAuthenticated = true,
                    // do something with claims here
                };
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }
    }
}
