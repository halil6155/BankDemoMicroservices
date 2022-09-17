namespace BanksDemo.Shared.Events.Abstract;

public interface ITransferSuccessfulEvent
{
    public string FromUserId
    { get; set; }
    public string ToUserId { get; set; }
    public Guid ProcessId { get; set; }
}