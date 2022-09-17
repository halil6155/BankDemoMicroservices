using BanksDemo.Shared.Events.Abstract;

namespace BanksDemo.Shared.Events.Concrete;

public class TransferEvent:ITransferEvent
{
    public TransferEvent(string fromFirstName, string fromLastName, string fromUserId, string toFirstName, decimal amount, string toLastName, string toUserId)
    {
        FromFirstName = fromFirstName;
        FromLastName = fromLastName;
        FromUserId = fromUserId;
        ToFirstName = toFirstName;
        Amount = amount;
        ToLastName = toLastName;
        ToUserId = toUserId;
    }

    public string FromFirstName { get; set; }
    public string FromLastName { get; set; }
    public string FromUserId { get; set; }
    public string ToFirstName { get; set; }
    public decimal Amount { get; set; }
    public string ToLastName { get; set; }
    public string ToUserId { get; set; }
}