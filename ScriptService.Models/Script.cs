using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models
{
	public class Script
	{
		public string Name { get; set; }

		public ScriptType Type { get; set; }

		public string Content { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}
}
