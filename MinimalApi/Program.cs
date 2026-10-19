using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Logger de Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddControllers();

DependencyProvider.Configure(builder.Services);

var app = builder.Build();

app.UseHttpsRedirection();

// Documentación OpenAPI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Mi API V1");
    });
}

app.UseGlobalExceptionHandler();

app.MapControllers();

// Inicialización según el proveedor seleccionado
await app.InitializationDatabaseAsync(AppConfig.Environment);

app.Run();