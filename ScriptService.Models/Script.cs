using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models
{
	public class Script
	{
		[Key]
		public int Id { get; set; }

		[NotNull]
		public string Name { get; set; }

		public ScriptType Type { get; set; }

		[ScriptContentMaxSize(150)]
		public string Content { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}
}
