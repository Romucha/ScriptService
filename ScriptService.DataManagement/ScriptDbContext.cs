using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScriptService.DataManagement.Configuration;
using ScriptService.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptService.DataManagement
{
    public class ScriptDbContext : IdentityDbContext
    {
        private readonly IConfiguration configuration;

        private readonly DbContextOptions<ScriptDbContext> dbContextOptions;

        public DbSet<Script> Scripts { get; set; }

        public ScriptDbContext(IConfiguration configuration, DbContextOptions<ScriptDbContext> dbContextOptions) : base(dbContextOptions)
        {
            this.configuration = configuration;
            this.dbContextOptions = dbContextOptions;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasPostgresEnum<ScriptType>();
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
