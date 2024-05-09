using Microsoft.OpenApi.Models;
using SOSRS.Api.Configuration;
using SOSRS.Api.Extensions;
using SOSRS.Api.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
            builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials();
            });
});

builder.Services.AddControllers().AddNewtonsoftJson(); ;

//builder.Services.AddEndpointsApiExplorer();]
builder.Services.ConfigureDependencies();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.ConfigureServices(builder.Configuration);
builder.Services.ConfigureAuth(builder.Configuration);
builder.Services.SincroniseDatabaseEF();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseCors();
app.MapControllers();

app.UseAuthorization();
app.UseMiddleware<AuthMiddleware>();

//app.MapAbrigoEndpoints();

app.Run();

