using BanksDemo.Wallet.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BanksDemo.Wallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;

        public WalletsController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _walletRepository.GetAllAsync());
        }
        [HttpGet("getByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var wallet = await _walletRepository.GetByUserIdAsync(userId);
            if (wallet == null)
                return NotFound();
            return Ok(wallet);
        }
    }
}
