﻿using ScriptService.Models.Data;
using ScriptService.Models.DTO.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.ComponentLibrary.Services
{
				public interface IScriptManagementService
				{
								Task<IEnumerable<GetScriptDTO>> GetAllScriptsAsync(string filter = null);

								Task<DetailScriptDTO> GetScriptByIdAsync(int id);

								Task DeleteScriptAsync(int id, string token);

								Task<Script> AddScriptAsync(Script script, string token);

								Task UpdateScriptAsync(Script script, string token);
				}
}
