using SOSRS.Api.Configuration;
using SOSRS.Api.Endpoints;
using SOSRS.Api.Services;

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

builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidadorService, ValidadorService>();

builder.Services.AddDatabaseConfiguration(builder.Configuration);
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

//app.MapAbrigoEndpoints();

app.Run();

