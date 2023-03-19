using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ScriptService.DataManagement;
using ScriptService.Models.Data;
using System.Text;

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

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var builder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = jwtSettings.GetSection("ValidAudience").Value,
                        ValidIssuer = jwtSettings.GetSection("ValidIssuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("IssuerSigningKey").Value))
                    };
                });
        }
    }
}
