using Application;
using Carter;
using Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


Log.Information("Order API starting");

// Add services to the container.

builder.Services.AddCarter();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

#pragma warning disable CA1305 // Specify IFormatProvider
builder.Host.UseSerilog((context, configuration) =>
    configuration.WriteTo.Console()
    .ReadFrom.Configuration(context.Configuration));
#pragma warning restore CA1305 // Specify IFormatProvider

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapCarter();

app.Run();
