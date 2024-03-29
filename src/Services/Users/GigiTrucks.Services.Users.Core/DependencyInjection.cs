﻿using System.Reflection;
using FluentValidation;
using GigiTrucks.Services.Users.Core.Auth;
using GigiTrucks.Services.Users.Core.DAL.EntityFramework;
using GigiTrucks.Services.Users.Core.DAL.Repositories;
using GigiTrucks.Services.Users.Core.Entities;
using GigiTrucks.Services.Users.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Users.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddSingleton(TimeProvider.System);

        var connectionString = configuration.GetConnectionString("Postgres");
        services.AddDbContext<UsersDbContext>(options => options.UseNpgsql(connectionString));
        services.AddHostedService<DbContextInitializer>();
        return services;
    }
}