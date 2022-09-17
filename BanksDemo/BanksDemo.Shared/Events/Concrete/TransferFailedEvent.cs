using BanksDemo.Shared.Events.Abstract;

namespace BanksDemo.Shared.Events.Concrete;

public class TransferFailedEvent: ITransferFailedEvent
{
    public TransferFailedEvent(Guid processId, string fromUserId, string toUserId, string exceptionMessage,decimal amount)
    {
       Amount = amount;
        ProcessId = processId;
        FromUserId = fromUserId;
        ToUserId = toUserId;
        ExceptionMessage = exceptionMessage;
    }

    public string FromUserId { get; set; }
    public string ExceptionMessage { get; set; }
    public string ToUserId { get; set; }
    public Guid ProcessId { get; set; }
    public decimal Amount { get; set; }
}