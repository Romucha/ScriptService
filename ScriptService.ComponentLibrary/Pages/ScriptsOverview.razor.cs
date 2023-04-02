using Microsoft.AspNetCore.Components;
using ScriptService.ComponentLibrary.Services;
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


    }
}
