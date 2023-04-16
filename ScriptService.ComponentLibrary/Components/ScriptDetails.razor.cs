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
								private ILocalStorageService localStorageService { get; set; }

								private Script _script { get; set; }

								[ParameterAttribute]
								public int Id { get; set; }

								protected override async Task OnInitializedAsync()
								{
												_script = Id > 0 ? await _scriptManagementService.GetScriptByIdAsync(Id) : new Script();
								}

								private async void OnSubmit()
								{
												if (_script != null)
												{
																if (Id > 0)
																{
																				await _scriptManagementService.UpdateScriptAsync(_script, await localStorageService.GetItemAsStringAsync("jwttoken"));
																}
																else
																{
																				await _scriptManagementService.AddScriptAsync(_script, await localStorageService.GetItemAsStringAsync("jwttoken"));
																}
																_navigationManager.NavigateTo("/scripts");
												}
								}
				}
}
