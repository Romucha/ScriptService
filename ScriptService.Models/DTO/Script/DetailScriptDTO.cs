using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models.DTO.Script
{
				public class DetailScriptDTO : CreateScriptDTO
				{
								public DateTime CreatedAt { get; set; }

								public DateTime UpdatedAt { get; set; }
				}
}
