using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ScriptService.ComponentLibrary.Services;
using ScriptService.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.ComponentLibrary.Pages
{
    public partial class ScriptsOverview
    {
        [Inject]
        private IScriptManagementService _scriptManagementService { get; set; }
								
        [Inject]
								private NavigationManager _navigationManager { get; set; }

								[Inject]
								private ILocalStorageService _localStorageService { get; set;	}

								public IEnumerable<Script> Scripts { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Scripts = await _scriptManagementService.GetAllScriptsAsync();
        }

        private async void ScriptClick(Script script)
        {
            _navigationManager.NavigateTo($"/scripts/{script.Id}");
        }

        private async Task DeleteScriptAsync(Script script)
        {
            await _scriptManagementService.DeleteScriptAsync(script.Id, await _localStorageService.GetItemAsStringAsync("jwttoken"));
												this.StateHasChanged();
        }

        private void AddScript()
        {
												_navigationManager.NavigateTo($"/scripts/0");
								}
    }
}
