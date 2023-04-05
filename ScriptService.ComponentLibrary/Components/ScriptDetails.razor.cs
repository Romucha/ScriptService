using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using ScriptService.ComponentLibrary.Services;
using ScriptService.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.ComponentLibrary.Components
{
				public partial class ScriptDetails
				{
								[Inject]
								private IScriptManagementService _scriptManagementService { get; set; }

								[Inject]
								private NavigationManager _navigationManager { get; set; }

								[Inject]
								private ILocalStorageService _localStorageService { get; set; }

								private Script _script { get; set; }

								[ParameterAttribute]
								public int Id { get; set; }

								protected override async Task OnInitializedAsync()
								{
												_script = await _localStorageService.GetItemAsync<Script>("SelectedScript");
								}

								private async void ResetSelectedScript()
								{
												await _localStorageService.ClearAsync();
								}
				}
}
