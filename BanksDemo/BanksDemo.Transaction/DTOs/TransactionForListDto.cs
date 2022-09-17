using BanksDemo.Shared.Interfaces;

namespace BanksDemo.Transaction.DTOs;

public class TransactionForListDto:IDto
{
    public Guid Id { get; set; }
    public string FromFirstName { get; set; }
    public string FromLastName { get; set; }
    public string FromUserId { get; set; }
    public string ToFirstName { get; set; }
 
    public decimal Amount { get; set; }
    public string ToLastName { get; set; }
    public string ToUserId { get; set; }
    public Guid ProcessId { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsComplete { get; set; }
    public string ProcessResult { get; set; }
}