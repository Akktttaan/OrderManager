using Api.Infrastructure.Logger;
using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Dal;
using Dal.Interfaces;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Ioc;

public static class IocConfig
{
    /// <summary>
    /// Метод расширения добавляющий конфигурацию проекта
    /// </summary>
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);

        return services;
    }
    
    /// <summary>
    /// Метод расширения добавляющий UnitOfWork
    /// </summary>
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services, [FromServices] IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("AppDbContext"));
        });
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }

    /// <summary>
    /// Метод расширения добавляющий сервисы приложения
    /// </summary>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var assembly = typeof(IService).Assembly;
        var serviceInterfaces = assembly
            .GetTypes()
            .Where(t => typeof(IService).IsAssignableFrom(t) && t != typeof(IService) && t.IsInterface);
        var servicePairs = serviceInterfaces
            .Select(x => new
            {
                serviceInterface = x,
                serviceImplementaion = assembly
                    .GetTypes()
                    .FirstOrDefault(x.IsAssignableFrom)
            });
        foreach (var servicePair in servicePairs)
        {
            if (servicePair.serviceImplementaion is not null)
                services.AddScoped(servicePair.serviceInterface, servicePair.serviceImplementaion);
        }
        
        return services;
    }

    /// <summary>
    /// Метод расширения добавляющий маппер AutoMapper
    /// </summary>
    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IBaseEntity));
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
    
    /// <summary>
    /// Метод расширения добавляющий сервис логирования
    /// </summary>
    public static IServiceCollection AddCustomerLogger(this IServiceCollection services)
    {
        services.AddTransient<ICustomLogger, NLogger>();

        return services;
    }

    /// <summary>
    /// Метод расширения добавляющий CORS
    /// </summary>
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder => builder.SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Content-Disposition")
                .AllowCredentials()));

        return services;
    }
}