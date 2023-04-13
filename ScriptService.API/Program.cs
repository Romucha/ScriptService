using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using ScriptService.API.Extensions;
using ScriptService.API.Services;
using ScriptService.DataManagement;
using ScriptService.DataManagement.Mapping;
using ScriptService.DataManagement.Repository;
using ScriptService.Models.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseKestrel();

builder.Services.AddControllers();
//Database
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
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MapperInitializer));

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//CORS
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowScriptHeaders", options =>
    {
        options.AllowAnyOrigin().AllowAnyMethod().WithHeaders(builder.Configuration.GetSection("CORS").GetValue<string>("Header"));
    });
    policy.AddDefaultPolicy(options =>
    {
        options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

//identity core
builder.Services.ConfigureIdentity();

//authentication and jwt
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.AddScoped<IAuthManager, AuthManager>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    ((IDbInitializer)scope.ServiceProvider.GetService(typeof(IDbInitializer))).Initialize();
}

app.Run();
