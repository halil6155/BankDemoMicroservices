using Microsoft.EntityFrameworkCore;

namespace BanksDemo.Wallet.Contexts;

public class WalletDbContext:DbContext
{
    public WalletDbContext(DbContextOptions<WalletDbContext> options):base(options)
    {
        
    }

    public DbSet<Models.Wallet> Wallets { get; set; }

}