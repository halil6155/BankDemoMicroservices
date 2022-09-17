using BanksDemo.Shared.Events.Concrete;
using MassTransit;

namespace BanksDemo.User.Consumers;

public class TransferFailedConsumer:IConsumer<TransferFailedEvent>
{
    public async Task Consume(ConsumeContext<TransferFailedEvent> context)
    {
        var transferFailedEvent = context.Message;
        //Loglama Yapılabilir.
        await Task.CompletedTask;
    }
}