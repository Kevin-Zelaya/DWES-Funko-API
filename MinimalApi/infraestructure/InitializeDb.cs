using MongoDB.Driver;
using Serilog;

public static class InitializationDb
{
    public static async Task InitializationDatabaseAsync(
        this WebApplication app,
        string contextType)
    {
        switch (contextType.ToLowerInvariant())
        {
            case "postgre":
                {
                    using var scope = app.Services.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    ///<summary> Eliminar base de datos <summary>
                    Log.Debug("[POSGRE] Eliminando y recreando la base de datos");
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.EnsureCreatedAsync();
                    Log.Information("[POSGRE] Eliminando y recreando la base de datos");
                    break;
                }
            case "mongoef":
                {
                    var client = new MongoClient(
                        AppConfig.ConnectionString);

                    Log.Warning("[MONGODB] Eliminando base de datos de pruebas");

                    await client.DropDatabaseAsync("tienda");

                    var database = client.GetDatabase("tienda");

                    await MongoContextConfiguration
                        .SeedSuppliersAsync(database);

                    Log.Information(
                        "[MONGODB] Base de datos reinicializada");

                    break;
                }
            case "mongo":
                {
                    Log.Warning("[MONGODB] Falta por aqui");
                    break;
                }
            default:
                throw new InvalidOperationException(
                    $"Proveedor no soportado: {contextType}");
        }
    }
}