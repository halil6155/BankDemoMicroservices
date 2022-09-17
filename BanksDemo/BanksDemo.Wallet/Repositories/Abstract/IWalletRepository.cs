using BanksDemo.Wallet.DTOs;

namespace BanksDemo.Wallet.Repositories.Abstract;

public interface IWalletRepository
{
    Task<int> CreateAsync(WalletForCreateDto walletForCreateDto);
    Task<List<WalletForListDto>> GetAllAsync();
    Task<WalletForListDto> GetByIdAsync(Guid id);
    Task<WalletForListDto?> GetByUserIdAsync(string userId);
    Task UpdateBalanceAsync(string userId, decimal balance);
}