using BanksDemo.Shared.Interfaces;

namespace BanksDemo.Wallet.DTOs;

public class WalletForCreateDto:IDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserId { get; set; }
}