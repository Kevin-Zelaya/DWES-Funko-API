

using MinimalApi.models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();



builder.Services.AddControllers();

DependencyProvider.Configure(
    builder.Services
);

var app = builder.Build();

app.UseHttpsRedirection();

// Documentación con swagger 
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/openapi/v1.json", "Mi API V1");
    });
}

// De momento sin
FunkoRepository repository = new FunkoRepository();

app.MapControllers();

app.Run();

