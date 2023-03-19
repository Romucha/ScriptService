using Microsoft.AspNetCore.Identity;
using ScriptService.DataManagement;
using ScriptService.Models.Data;

namespace ScriptService.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ScriptUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });
            builder = new IdentityBuilder(builder.UserType, services);
            builder.AddRoles<IdentityRole>().AddEntityFrameworkStores<ScriptDbContext>().AddDefaultTokenProviders();
        }
    }
}
