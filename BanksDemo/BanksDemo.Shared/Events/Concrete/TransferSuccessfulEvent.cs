using BanksDemo.Shared.Events.Abstract;

namespace BanksDemo.Shared.Events.Concrete;

public class TransferSuccessfulEvent: ITransferSuccessfulEvent
{
    public TransferSuccessfulEvent(string fromUserId, string toUserId, Guid processId)
    {
        FromUserId = fromUserId;
        ToUserId = toUserId;
        ProcessId = processId;
    }

    public string FromUserId { get; set; }
    public string ToUserId { get; set; }
    public Guid ProcessId { get; set; }
}