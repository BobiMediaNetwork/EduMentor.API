using EduMentor.Application.Interfaces.Repositories;
using EduMentor.Persistence.Context;
using EduMentor.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EduMentor.Infrastructure.Services;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config, Assembly[] coreProjectsAssemblies)
    {
        services.AddDbContext<EduMentorDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("EduMentor"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(coreProjectsAssemblies)
        );

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}