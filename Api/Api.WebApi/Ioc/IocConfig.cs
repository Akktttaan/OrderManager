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
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);

        return services;
    }
    
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services, [FromServices] IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("AppDbContext"));
        });
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }

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

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IBaseEntity));
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}