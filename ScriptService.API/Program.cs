using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using ScriptService.DataManagement;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ScriptDbContext>(options =>
{
	string connString = string.Empty;
	if (builder.Environment.IsDevelopment())
	{
		connString = "Dev";
	}
	else if (builder.Environment.IsProduction())
	{
		connString = "Prod";
	}
	else if (builder.Environment.IsStaging())
	{
		connString = "Stage";
	}
	else
	{
		connString = "Default";
	}
	options.UseNpgsql(builder.Configuration.GetConnectionString(connString));
});
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy =>
{
				policy.AddPolicy("AllowAll", options =>
				{
								options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
				});
});

builder.Services.AddIdentityCore<ScriptUser>(u =>
{
	u.User.RequireUniqueEmail = true;
})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ScriptDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
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
			ValidAudience = builder.Configuration.GetSection("Jwt").GetSection("ValidAudience").Value,
			ValidIssuer = builder.Configuration.GetSection("Jwt").GetSection("ValidIssuer").Value,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt").GetSection("IssuerSigningKey").Value))
		};
	});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors();
using (var scope = app.Services.CreateScope())
{
	((IDbInitializer)scope.ServiceProvider.GetService(typeof(IDbInitializer))).Initialize();
}

app.Run();
