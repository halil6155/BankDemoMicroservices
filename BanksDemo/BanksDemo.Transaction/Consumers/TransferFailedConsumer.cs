using BanksDemo.Shared.Events.Abstract;
using BanksDemo.Transaction.Repositories.Abstract;
using MassTransit;

namespace BanksDemo.Transaction.Consumers;

public class TransferFailedConsumer:IConsumer<ITransferFailedEvent>
{
    private readonly ITransactionRepository _transactionRepository;

    public TransferFailedConsumer(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task Consume(ConsumeContext<ITransferFailedEvent> context)
    {
        var result = await _transactionRepository.UpdateByOrderNumberForTransferResultAsync
            (context.Message.ProcessId, context.Message.ExceptionMessage, false);
        if (!result)
        {
            //İşlem Tekrarlanmalı Ve Loglama
        }
    }
}