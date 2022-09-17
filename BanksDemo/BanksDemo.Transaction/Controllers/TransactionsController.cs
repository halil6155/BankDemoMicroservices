using BanksDemo.Transaction.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BanksDemo.Transaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _transactionRepository.GetAllAsync());
        }
        [HttpGet("getByFromUserId/{fromUserId}")]
        public async Task<IActionResult> GetByFromUserId(string fromUserId)
        {
            var result = await _transactionRepository.GetListByFromUserIdAsync(fromUserId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpGet("getByToUserId/{toUserId}")]
        public async Task<IActionResult> GetByToUserId(string toUserId)
        {
            var result = await _transactionRepository.GetListByToUserIdAsync(toUserId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpGet("getByOrderNumber/{orderNumber}")]
        public async Task<IActionResult> GetByOrderUserId(string orderNumber)
        {
            var result = await _transactionRepository.GetByOrderNumberAsync(Guid.Parse(orderNumber));
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
