using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScriptService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.DataManagement
{
	public class ScriptDbContext : DbContext
	{
		public DbSet<Script> Scripts { get; set; }

		public ScriptDbContext()
		{

		}

		protected override void OnModelCreating(ModelBuilder builder) 
		{ 
			builder.HasPostgresEnum<ScriptType>();
		}
	}
}
