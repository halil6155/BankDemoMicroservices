using BanksDemo.Shared.Constants;
using BanksDemo.Shared.Settings;
using BanksDemo.User.BusinessRules;
using BanksDemo.User.Consumers;
using BanksDemo.User.Contexts;
using BanksDemo.User.Repositories.Abstract;
using BanksDemo.User.Repositories.Concrete;
using BanksDemo.User.Services.Abstract;
using BanksDemo.User.Services.Concrete;
using BanksDemo.User.Settings;
using MassTransit;
using Microsoft.Extensions.Options;

namespace BanksDemo.User.Extensions;

public static class ServiceBuilderExtensions
{
    public static void AddHttpClientByConfiguration(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddHttpClient<IWalletService,WalletManager>(configure =>
        {
            configure.BaseAddress = new Uri(configuration.GetSection("ServiceAddress")["WalletService"]);
        });
    }
    public static void AddBusinessRules(this IServiceCollection services)
    {
        services.AddScoped<UserTransferRules>();
    }
    public static void AddMassTransitRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqConfigurationSettings))
            .Get<RabbitMqConfigurationSettings>();
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<TransferFailedConsumer>();
            configurator.AddConsumer<WalletFailEventConsumer>();
            configurator.UsingRabbitMq((context, configure) =>
            {
                configure.Host(rabbitMqConfiguration.Host, cfg =>
                {
                    cfg.Username(rabbitMqConfiguration.UserName);
                    cfg.Password(rabbitMqConfiguration.Password);
                });
                configure.ReceiveEndpoint(RabbitMqConstants.WalletFailCreateEventQueueName, configureEndpoint =>
                {
                    configureEndpoint.ConfigureConsumer<WalletFailEventConsumer>(context);
                });
                configure.ReceiveEndpoint(RabbitMqConstants.TransferFailedQueueName, configureEndpoint =>
                {
                    configureEndpoint.ConfigureConsumer<TransferFailedConsumer>(context);
                });
            });
        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserContext, UserContext>();
        services.AddTransient<IUserRepository, UserRepository>();
 
    }

    public static void AddConfigurationForDb(this IServiceCollection services, IConfiguration configuration)
    {
        //DatabaseConfigurationSetting
        services.Configure<DatabaseConfigurationSetting>(configuration.GetSection(nameof(DatabaseConfigurationSetting)));
        services.AddSingleton<IDatabaseConfigurationSettings>(x =>
            x.GetRequiredService<IOptions<DatabaseConfigurationSetting>>().Value);
    }
}