using System.Data;
using AutoMapper;
using BanksDemo.Transaction.Constants;
using BanksDemo.Transaction.DTOs;
using BanksDemo.Transaction.Repositories.Abstract;
using Dapper;
namespace BanksDemo.Transaction.Repositories.Concrete;

public class TransactionRepository:ITransactionRepository
{
    private const string TableName = "Transactions";
    private readonly IDbConnection _connection;
    private readonly IMapper _mapper;

    public TransactionRepository(IDbConnection connection, IMapper mapper)
    {
        _connection = connection;
        
        _mapper = mapper;
    }

    public async Task<Models.Transaction> CreateAsync(TransactionForAddDto transactionForAddDto)
    {
        var transaction = _mapper.Map<Models.Transaction>(transactionForAddDto);
        transaction.CreatedOn=DateTime.UtcNow;
        transaction.Id=Guid.NewGuid();
       transaction.ProcessId=Guid.NewGuid();
       transaction.ProcessResult = DatabaseMessages.TransferPending;
        var sql =
            $"Insert into {TableName} VALUES (@Id,@FromFirstName,@FromLastName,@FromUserId,@ToFirstName,@Amount,@ToLastName,@ToUserId,@ProcessId,@CreatedOn,@ProcessResult,@IsComplete)";
      await _connection.ExecuteAsync(sql, transaction);
      return transaction;
    }

    public async Task<List<TransactionForListDto>> GetAllAsync()
    {
        var transactions =await _connection.QueryAsync<Models.Transaction>($"select * from {TableName}");
        return _mapper.Map<List<TransactionForListDto>>(transactions);
    }

    public async Task<TransactionForListDto?> GetByIdAsync(Guid id)
    {
        var transaction = await _connection.QueryFirstOrDefaultAsync<Models.Transaction>("select * from {TableName} where Id=@id",new{id});
        return _mapper.Map<TransactionForListDto>(transaction);
    }

    public async Task<TransactionForListDto?> GetByOrderNumberAsync(Guid orderNumber)
    {
        var transaction = await _connection.QueryFirstOrDefaultAsync<Models.Transaction>("select * from {TableName} where ProcessId=@orderNumber",new{orderNumber});
        return _mapper.Map<TransactionForListDto>(transaction);
    }

    public async Task<List<TransactionForListDto>?> GetListByFromUserIdAsync(string userId)
    {
        var transactions = await _connection.QueryAsync<Models.Transaction>($"select * from {TableName} where FromUserId=@fromUserId",new{userId});
        return _mapper.Map<List<TransactionForListDto>>(transactions);
    }

    public async Task<List<TransactionForListDto>?> GetListByToUserIdAsync(string userId)
    {
        var transactions = await _connection.QueryAsync<Models.Transaction>($"select * from {TableName} where ToUserId=@toUserId", new { userId });
        return _mapper.Map<List<TransactionForListDto>>(transactions);
    }

    public async Task<bool> UpdateByOrderNumberForTransferResultAsync(Guid orderNumber, string processResult, bool completed)
    {
        
        var sql = $"UPDATE {TableName} SET processresult = @processResult, IsComplete = @completed WHERE processId = @orderNumber";
        var result = await _connection.ExecuteAsync(sql,
            new {processResult,  completed, orderNumber });
        return result > 0;
    }
}