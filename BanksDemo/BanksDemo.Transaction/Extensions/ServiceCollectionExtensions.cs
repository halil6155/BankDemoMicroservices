using System.Data;
using BanksDemo.Shared.Constants;
using BanksDemo.Shared.Settings;
using BanksDemo.Transaction.Consumers;
using BanksDemo.Transaction.Repositories.Abstract;
using BanksDemo.Transaction.Repositories.Concrete;
using MassTransit;
using Microsoft.Data.SqlClient;

namespace BanksDemo.Transaction.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddTransactionDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnection>(o => new SqlConnection(configuration.GetConnectionString("TransactionDbContext")));
    }
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();
    }
    public static void AddMassTransitRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqConfigurationSettings))
            .Get<RabbitMqConfigurationSettings>();
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<TransferFailedConsumer>();
            configurator.AddConsumer<TransferRequestConsumer>();
            configurator.AddConsumer<TransferSuccessfulConsumer>();
            configurator.UsingRabbitMq((context, configure) =>
            {
                configure.Host(rabbitMqConfiguration.Host, cfg =>
                {
                    cfg.Username(rabbitMqConfiguration.UserName);
                    cfg.Password(rabbitMqConfiguration.Password);
                });
                configure.ReceiveEndpoint(RabbitMqConstants.TransferRequestQueueName, configureEndpoint =>
                {
                    configureEndpoint.ConfigureConsumer<TransferRequestConsumer>(context);
                });
                configure.ReceiveEndpoint(RabbitMqConstants.TransferSuccessQueueName, configureEndpoint =>
                {
                    configureEndpoint.ConfigureConsumer<TransferSuccessfulConsumer>(context);
                });
                configure.ReceiveEndpoint(RabbitMqConstants.TransferFailedQueueName, configureEndpoint =>
                {
                    configureEndpoint.ConfigureConsumer<TransferFailedConsumer>(context);
                });
            });
        });
    }
}