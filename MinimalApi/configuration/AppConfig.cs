public static class AppConfig
{
    static AppConfig()
    {
        Environment = 
            System.Environment
                .GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                .ToLowerInvariant() 
            ?? "posgre";
            
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(
                "appsettings.json",
                optional: false,
                reloadOnChange: true
            )
            .AddJsonFile(
                 $"appsettings.{(Environment is "mongoef" or "mongo" ? "Mongo" : Environment)}.json",
                optional: false,
                reloadOnChange: true
            )
            .Build();
        ConnectionString = Configuration["ConnectionStrings:DefaultConnection"]
            ?? throw new ArgumentException("Error al obtener la propiedad ConnectionStrings:DefaultConnection");
    }

    public static IConfiguration Configuration {get; private set;}
    public static string ConnectionString {get; private set;}

    public static string? Environment {get; private set;}
} 