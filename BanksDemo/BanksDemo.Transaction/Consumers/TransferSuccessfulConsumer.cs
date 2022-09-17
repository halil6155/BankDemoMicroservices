using BanksDemo.Shared.Events.Abstract;
using BanksDemo.Transaction.Constants;
using BanksDemo.Transaction.Repositories.Abstract;
using MassTransit;

namespace BanksDemo.Transaction.Consumers;

public class TransferSuccessfulConsumer : IConsumer<ITransferSuccessfulEvent>
{
    private readonly ITransactionRepository _transactionRepository;

    public TransferSuccessfulConsumer(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task Consume(ConsumeContext<ITransferSuccessfulEvent> context)
    {
        var result = await _transactionRepository.UpdateByOrderNumberForTransferResultAsync(context.Message.ProcessId,
              DatabaseMessages.TransferCompleted,true);
        if (!result)
        {
            //İşlem Tekrarlanmalı Ve Loglama
        }
    }
}