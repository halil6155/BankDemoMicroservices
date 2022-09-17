using BanksDemo.Transaction.DTOs;

namespace BanksDemo.Transaction.Repositories.Abstract;

public interface ITransactionRepository
{
    Task<Models.Transaction> CreateAsync(TransactionForAddDto transactionForAddDto);
    Task<List<TransactionForListDto>> GetAllAsync();
    Task<TransactionForListDto?> GetByIdAsync(Guid id);
    Task<TransactionForListDto?> GetByOrderNumberAsync(Guid orderNumber);
    Task<List<TransactionForListDto>?> GetListByFromUserIdAsync(string userId);
    Task<List<TransactionForListDto>?> GetListByToUserIdAsync(string userId);
    Task<bool> UpdateByOrderNumberForTransferResultAsync(Guid orderNumber, string processResult,bool completed);
}