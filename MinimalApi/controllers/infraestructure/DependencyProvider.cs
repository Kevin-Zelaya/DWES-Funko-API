using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class DependencyProvider
{
    public static void Configure(
        IServiceCollection _services
    )
    {
        
        _services.Scan(scan => scan
            .FromAssemblyOf<FunkoController>()
            .AddClasses(classes => classes.AssignableTo<FunkoController>())
            .AsSelf()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo<FunkoRepository>())
            .AsSelf()
            .WithScopedLifetime());

        _services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(AppConfig.StringConnection));
    }
}