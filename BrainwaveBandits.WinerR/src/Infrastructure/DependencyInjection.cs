﻿using BrainwaveBandits.WinerR.Application.Common.Interfaces;
using BrainwaveBandits.WinerR.Infrastructure.Data;
using BrainwaveBandits.WinerR.Infrastructure.Data.Configurations;
using BrainwaveBandits.WinerR.Infrastructure.Data.Interceptors;
using BrainwaveBandits.WinerR.Infrastructure.Identity;
using BrainwaveBandits.WinerR.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services
            .AddDefaultIdentity<ApplicationUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddSingleton(TimeProvider.System);
        services.AddTransient<IIdentityService, IdentityService>();

        services.AddScoped<IOpenAIService, OpenAIService>();
        services.Configure<OpenAiOptions>(configuration.GetSection("OpenAI"));


        services.Configure<RecommenderSystemOptions>(configuration.GetSection("RecommenderSystemOptions"));
        services.AddHttpClient<IRecommenderService, RecommenderService>();



        return services;
    }
}
