using BanksDemo.Shared.Events.Abstract;
using BanksDemo.User.Repositories.Abstract;
using MassTransit;

namespace BanksDemo.User.Consumers;

public class WalletFailEventConsumer:IConsumer<IFailWalletCreated>
{
    private readonly IUserRepository _userRepository;

    public WalletFailEventConsumer(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<IFailWalletCreated> context)
    {
        await _userRepository.DeleteAsync(context.Message.UserId);
    }
}