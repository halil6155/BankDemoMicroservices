namespace BanksDemo.Shared.Events.Abstract;

public interface ITransferCompleteEvent
{
    public string FromUserId { get; set; }
    public Guid ProcessId { get; set; }
    public string ToUserId { get; set; }
    public decimal Balance { get; set; }
}