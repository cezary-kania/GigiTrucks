using GigiTrucks.Services.Common.Messaging;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GigiTrucks.Services.Carts.Infrastructure.Messaging;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<RabbitMQSettings>(configuration.GetSection(nameof(RabbitMQSettings)));
        var rabbitMQSetting = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitMQSettings>>().Value;
        services.AddMassTransit(x =>
        {
            x.AddConsumers(GetConsumers());
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

    private static Type[] GetConsumers() 
        => AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IIntegrationEventHandler).IsAssignableFrom(t))
            .ToArray();
}