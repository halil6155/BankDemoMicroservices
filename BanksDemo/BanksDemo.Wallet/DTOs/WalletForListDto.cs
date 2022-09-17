using BanksDemo.Shared.Interfaces;

namespace BanksDemo.Wallet.DTOs;

public class WalletForListDto:IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public decimal Balance { get; set; }
    public string UserId { get; set; }
    public bool IsLock { get; set; }
}