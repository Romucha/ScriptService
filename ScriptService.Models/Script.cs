using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.Models
{
	public class Script
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public ScriptType Type { get; set; }

		public string Content { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }
	}
}
