using BanksDemo.Shared.Constants;
using BanksDemo.Shared.Settings;
using BanksDemo.Wallet.BusinessRules;
using BanksDemo.Wallet.Consumers;
using BanksDemo.Wallet.Contexts;
using BanksDemo.Wallet.Repositories.Abstract;
using BanksDemo.Wallet.Repositories.Concrete;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BanksDemo.Wallet.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddBusinessRules(this IServiceCollection services)
    {
        services.AddScoped<UserWalletRules>();
    }
    public static void AddMassTransitRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqConfigurationSettings))
            .Get<RabbitMqConfigurationSettings>();
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<UserCreatedEventConsumer>();
            configurator.AddConsumer<TransferCompleteRequestConsumer>();
            configurator.UsingRabbitMq((context, configure) =>
            {
                configure.Host(rabbitMqConfiguration.Host, cfg =>
                    {
                        cfg.Username(rabbitMqConfiguration.UserName);
                        cfg.Password(rabbitMqConfiguration.Password);
                    }
                );
                configure.ReceiveEndpoint(RabbitMqConstants.UserCreatedEventQueueName, configureEndpoint =>
                {
                    configureEndpoint.ConfigureConsumer<UserCreatedEventConsumer>(context);
                });
                configure.ReceiveEndpoint(RabbitMqConstants.WalletTransferRequestQueueName, configureEndpoint =>
                {
                    configureEndpoint.ConfigureConsumer<TransferCompleteRequestConsumer>(context);
                });
            });
        });
    }
    public static void AddWalletDb(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<WalletDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("WalletDbContext"));
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWalletRepository, WalletRepository>();
    }
}