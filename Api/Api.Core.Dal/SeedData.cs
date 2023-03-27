using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dal;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();

        // Применяем все миграции, которых еще нет в базе данных
        context.Database.Migrate();
        
        FillProviders(context);
    }

    private static void FillProviders(AppDbContext context)
    {
        if (context.Providers.Any()) return;
        context.Providers.AddRange(
            new Provider { Name = "Provider 1" },
            new Provider { Name = "Provider 2" },
            new Provider { Name = "Provider 3" }
        );

        context.SaveChanges();
    }
}