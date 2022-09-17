using AutoMapper;
using BanksDemo.Wallet.Contexts;
using BanksDemo.Wallet.DTOs;
using BanksDemo.Wallet.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BanksDemo.Wallet.Repositories.Concrete;

public class WalletRepository:IWalletRepository
{
    private readonly IMapper _mapper;
    private readonly WalletDbContext _walletDbContext;

    public WalletRepository(WalletDbContext walletDbContext, IMapper mapper)
    {
        _walletDbContext = walletDbContext;
        _mapper = mapper;
    }

    public async Task<int> CreateAsync(WalletForCreateDto walletForCreateDto)
    {
        var wallet = _mapper.Map<Models.Wallet>(walletForCreateDto);
        wallet.IsLock = false;
        _walletDbContext.Wallets.Add(wallet);
        return await _walletDbContext.SaveChangesAsync();
    }

    public async Task<List<WalletForListDto>> GetAllAsync()
    {
        var wallets = await _walletDbContext.Wallets.AsNoTracking().ToListAsync();
        return _mapper.Map<List<WalletForListDto>>(wallets);
    }

    public async Task<WalletForListDto> GetByIdAsync(Guid id)
    {
        var wallet =await  _walletDbContext.Wallets.FindAsync(id);
        return _mapper.Map<WalletForListDto>(wallet);
    }

    public async Task<WalletForListDto?> GetByUserIdAsync(string userId)
    {
        var wallet = await _walletDbContext.Wallets.FirstOrDefaultAsync(x=>x.UserId== userId);
        return _mapper.Map<WalletForListDto>(wallet);
    }

    public async Task UpdateBalanceAsync(string userId, decimal balance)
    {
        var wallet = await _walletDbContext.Wallets.FirstOrDefaultAsync(x=>x.UserId==userId);
        if (wallet == null) return;
        wallet.Balance = balance;
        await _walletDbContext.SaveChangesAsync();
    }
}