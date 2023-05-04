using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using ScriptService.ComponentLibrary.Services;
using ScriptService.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
								public int? id { get; set; }

								protected override async Task OnInitializedAsync()
								{
												if (id == null)
												{
																_script = new Script();
												}
												else
												{
																int _id = (int)id;
																_script = JsonSerializer.Deserialize<Script>(JsonSerializer.Serialize(await _scriptManagementService.GetScriptByIdAsync(_id)));
												}
								}

								private async void OnSubmit()
								{
												if (_script != null)
												{
																if (id > 0)
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
