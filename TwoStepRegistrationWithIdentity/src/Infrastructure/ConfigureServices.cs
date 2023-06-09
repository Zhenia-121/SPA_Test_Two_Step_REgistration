﻿using Application.Common.Configurations;
using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence.Sql.Contexts;
using Infrastructure.Persistence.Sql.Interceptors;
using Infrastructure.Persistence.Sql.Seed;
using Infrastructure.Services.DateTime;
using Infrastructure.Services.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        #region Persistence DI

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TwoStepRegistrationWithIdentity"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<DatabaseInitializer>();

        services.Configure<PasswordRequirements>(configuration.GetSection(nameof(PasswordRequirements)));

        var passwordRequirements = configuration.GetSection(nameof(PasswordRequirements)).Get<PasswordRequirements>();

        services
            .AddDefaultIdentity<ApplicationUser>(options => {
                options.Password.RequireDigit = passwordRequirements.RequireDigit;
                options.Password.RequiredLength = passwordRequirements.RequiredLength;
                options.Password.RequireNonAlphanumeric = passwordRequirements.RequireNonAlphanumeric;
                options.Password.RequireUppercase = passwordRequirements.RequireUppercase;
                options.Password.RequireLowercase = passwordRequirements.RequireLowercase;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddIdentityServer()
            .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

        #endregion

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddAuthentication()
            .AddIdentityServerJwt();

        //services.AddAuthorization(options =>
        //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

        return services;
    }
}
