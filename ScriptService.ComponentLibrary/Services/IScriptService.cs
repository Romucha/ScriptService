﻿using ScriptService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.ComponentLibrary.Services
{
	public interface IScriptManagementService
	{
		Task<IEnumerable<Script>> GetAllScriptsAsync(string filter = null);

		Task<Script> GetScriptByIdAsync(int id);

		Task DeleteScriptAsync(int id);

		Task<Script> AddScriptAsync(Script script);

		Task UpdateScriptAsync(Script script);
	}
}