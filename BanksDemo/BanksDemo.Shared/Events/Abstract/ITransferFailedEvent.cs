namespace BanksDemo.Shared.Events.Abstract;

public interface ITransferFailedEvent
{
    public string FromUserId
    { get; set; }

    public string ExceptionMessage { get; set; }
    public string ToUserId { get; set; }
    public Guid ProcessId { get; set; }
    public decimal Amount { get; set; }
}