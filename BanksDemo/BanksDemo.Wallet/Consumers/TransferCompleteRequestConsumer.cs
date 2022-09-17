using BanksDemo.Shared.Constants;
using BanksDemo.Shared.Events.Abstract;
using BanksDemo.Shared.Events.Concrete;
using BanksDemo.Shared.Helpers;
using BanksDemo.Wallet.BusinessRules;
using BanksDemo.Wallet.Repositories.Abstract;
using MassTransit;

namespace BanksDemo.Wallet.Consumers;

public class TransferCompleteRequestConsumer : IConsumer<ITransferCompleteEvent>
{
    private readonly IWalletRepository _walletRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly UserWalletRules _userWalletRules;
    public TransferCompleteRequestConsumer(IWalletRepository walletRepository, UserWalletRules userWalletRules, ISendEndpointProvider sendEndpointProvider)
    {
        _walletRepository = walletRepository;
        _userWalletRules = userWalletRules;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async Task Consume(ConsumeContext<ITransferCompleteEvent> context)
    {
        var fromUser = await _walletRepository.GetByUserIdAsync(context.Message.FromUserId);
        var toUser = await _walletRepository.GetByUserIdAsync(context.Message.ToUserId);
        var errorMessage = BusinessRulesHelper.CheckRules(
            _userWalletRules.WalletCheckByWalletListDtoForTransfer(fromUser),
            _userWalletRules.WalletCheckByWalletListDtoForTransfer(toUser),
            _userWalletRules.WalletAmountCheck(fromUser?.Balance ?? -1, context.Message.Balance)
        );
        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(
            string.IsNullOrEmpty(errorMessage) ?
                new Uri($"queue:{RabbitMqConstants.TransferSuccessQueueName}") :
            new Uri($"queue:{RabbitMqConstants.TransferFailedQueueName}"));
        if (!string.IsNullOrEmpty(errorMessage))
        {
            await sendEndpoint.Send(new TransferFailedEvent(context.Message.ProcessId, context.Message.FromUserId,
              context.Message.ToUserId, errorMessage
              ,context.Message.Balance));
            return;
        }

        await _walletRepository.UpdateBalanceAsync(fromUser!.UserId, fromUser.Balance - context.Message.Balance);
        await _walletRepository.UpdateBalanceAsync(toUser!.UserId, toUser.Balance + context.Message.Balance);
        await sendEndpoint.Send(new TransferSuccessfulEvent(context.Message.FromUserId, context.Message.FromUserId,
            context.Message.ProcessId));
    }
}