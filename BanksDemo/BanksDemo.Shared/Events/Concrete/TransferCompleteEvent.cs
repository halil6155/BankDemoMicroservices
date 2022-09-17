using BanksDemo.Shared.Events.Abstract;

namespace BanksDemo.Shared.Events.Concrete;

public class TransferCompleteEvent:ITransferCompleteEvent
{
    public TransferCompleteEvent(string fromUserId, string toUserId, decimal balance,Guid processId)
    {
        FromUserId = fromUserId;
        ToUserId = toUserId;
        Balance = balance;
        ProcessId = processId;
    }

    public string FromUserId { get; set; }
    public Guid ProcessId { get; set; }
    public string ToUserId { get; set; }
    public decimal Balance { get; set; }
}