using BanksDemo.User.Models;

namespace BanksDemo.User.Services.Abstract;

public interface IWalletService
{
    Task<WalletListDto?> GetWalletByUserIdAsync(string userId);
}