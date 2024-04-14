using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GigiTrucks.Services.Newsletter.Infrastructure.Messaging;

public static class Extensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<RabbitMQSettings>(configuration.GetSection($"Messaging:{nameof(RabbitMQSettings)}"));
        var rabbitMQSetting = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitMQSettings>>().Value;
        services.AddMassTransit(x =>
        {
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
}