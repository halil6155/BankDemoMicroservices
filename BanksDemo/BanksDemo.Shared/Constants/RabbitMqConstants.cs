namespace BanksDemo.Shared.Constants;

public class RabbitMqConstants
{
    public  const string UserCreatedEventQueueName="user-created-event";
    public const string WalletFailCreateEventQueueName = "wallet-fail-created";
    public const string TransferRequestQueueName = "transfer-request";
    public const string WalletTransferRequestQueueName = "wallet-transfer-request";
    public const string TransferFailedQueueName = "transfer-failed";
    public const string TransferSuccessQueueName = "transfer-success";

}