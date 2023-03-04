using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.DataManagement
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ScriptDbContext _dbContext;

		private readonly ILogger _logger;

		public DbInitializer(ScriptDbContext repoDbContext, ILogger<DbInitializer> logger)
		{
			_dbContext = repoDbContext;
			_logger = logger;
		}
		public void Initialize()
		{
			try
			{
				if (_dbContext.Database.GetPendingMigrations().Count() > 0)
				{
					_dbContext.Database.Migrate();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}
		}
	}
}
