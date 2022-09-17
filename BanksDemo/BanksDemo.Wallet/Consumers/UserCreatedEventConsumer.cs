using AutoMapper;
using BanksDemo.Shared.Events.Abstract;
using BanksDemo.Shared.Events.Concrete;
using BanksDemo.Wallet.DTOs;
using BanksDemo.Wallet.Repositories.Abstract;
using MassTransit;

namespace BanksDemo.Wallet.Consumers;

public class UserCreatedEventConsumer : IConsumer<IUserCreatedEvent>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;
    public UserCreatedEventConsumer(IWalletRepository walletRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _walletRepository = walletRepository;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<IUserCreatedEvent> context)
    {
        var addedEntityCount = await _walletRepository.CreateAsync(_mapper.Map<WalletForCreateDto>(context.Message));
        if (addedEntityCount < 1)
            await _publishEndpoint.Publish(new FailWalletCreated(context.Message.UserId));
    }
}