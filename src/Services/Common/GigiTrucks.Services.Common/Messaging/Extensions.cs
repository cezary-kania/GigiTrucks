using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GigiTrucks.Services.Common.Messaging;

public static class Extensions
{
    public static IServiceCollection AddMessaging(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
        var rabbitMQSetting = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitMQSettings>>().Value;
        services.AddMassTransit(x =>
        {
            x.AddConsumers(GetAppAssemblies());
            x.SetKebabCaseEndpointNameFormatter();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMQSetting.Host, "/", h =>
                {
                    h.Username(rabbitMQSetting.Username);
                    h.Password(rabbitMQSetting.Password);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }

    private static Assembly[] GetAppAssemblies() 
        => AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => x.FullName.StartsWith(nameof(GigiTrucks)))
            .ToArray();
}