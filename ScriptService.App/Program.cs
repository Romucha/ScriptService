using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using NLog.Fluent;
using ScriptService.App;
using ScriptService.App.Services;
using ScriptService.ComponentLibrary.Services;

try
{
				var builder = WebAssemblyHostBuilder.CreateDefault(args);

				builder.RootComponents.Add<App>("#app");
				builder.RootComponents.Add<HeadOutlet>("head::after");

				builder.Services.AddMudServices();

				builder.Services.AddBlazoredLocalStorage();

				builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


				builder.Services.AddHttpClient<IScriptAccountService, ScriptAccountService>(client =>
				client.BaseAddress = new Uri(builder.Configuration["ScriptServiceUrl"]));
				builder.Services.AddOptions();
				builder.Services.AddAuthorizationCore();
				builder.Services.AddScoped<ScriptAuthenticationStateProvider>();
				builder.Services.AddScoped<AuthenticationStateProvider>(c => (AuthenticationStateProvider)c.GetRequiredService(typeof(ScriptAuthenticationStateProvider)));

				builder.Services.AddHttpClient<IScriptManagementService, ScriptManagementService>(client =>
				client.BaseAddress = new Uri(builder.Configuration["ScriptServiceUrl"]));

				var app = builder.Build();


				await app.RunAsync();
}
catch (Exception ex)
{
				NLog.LogManager.GetCurrentClassLogger().Fatal(ex, "Failed to start Blazor app");
}
finally
{
				NLog.LogManager.Flush();
}
