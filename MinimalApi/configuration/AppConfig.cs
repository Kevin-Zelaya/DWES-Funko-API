public static class AppConfig
{
    static AppConfig()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(
                "appsettings.json",
                optional: false,
                reloadOnChange: true
            )
            .Build();
    }

    public static IConfiguration Configuration {get; private set;}
    public static string StringConnection {get; private set;}
} 