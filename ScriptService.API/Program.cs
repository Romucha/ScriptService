using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using ScriptService.DataManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
using (var scope = app.Services.CreateScope())
{
	((IDbInitializer)scope.ServiceProvider.GetService(typeof(IDbInitializer))).Initialize();
}

app.Run();
