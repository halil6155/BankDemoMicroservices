namespace BanksDemo.Shared.Events.Abstract;

public interface ITransferEvent
{
    public string FromFirstName { get; set; }
    public string FromLastName { get; set; }
    public string FromUserId { get; set; }
    public string ToFirstName { get; set; }
    public decimal Amount { get; set; }
    public string ToLastName { get; set; }
    public string ToUserId { get; set; }
}