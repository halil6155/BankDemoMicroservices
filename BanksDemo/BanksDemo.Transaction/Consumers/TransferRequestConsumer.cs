using AutoMapper;
using BanksDemo.Shared.Events.Abstract;
using BanksDemo.Shared.Events.Concrete;
using BanksDemo.Transaction.DTOs;
using BanksDemo.Transaction.Repositories.Abstract;
using MassTransit;

namespace BanksDemo.Transaction.Consumers;

public class TransferRequestConsumer : IConsumer<ITransferEvent>
{
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ITransactionRepository _transactionRepository;
    public TransferRequestConsumer(IMapper mapper, ITransactionRepository transactionRepository, IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _transactionRepository = transactionRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<ITransferEvent> context)
    {
        var transferForAddDto = _mapper.Map<TransactionForAddDto>(context.Message);
       var addedTransaction= await _transactionRepository.CreateAsync(transferForAddDto);
      await  _publishEndpoint.Publish(new TransferCompleteEvent(context.Message.FromUserId, context.Message.ToUserId,
            context.Message.Amount, addedTransaction.ProcessId));
    }
}