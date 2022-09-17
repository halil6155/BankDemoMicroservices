using AutoMapper;
using BanksDemo.Shared.Events.Abstract;
using BanksDemo.Transaction.DTOs;

namespace BanksDemo.Transaction.Mapping;

public class TransactionProfile:Profile
{
    public TransactionProfile()
    {
        CreateMap<ITransferEvent, TransactionForAddDto>();
        CreateMap<TransactionForAddDto,Models.Transaction>();
        CreateMap<Models.Transaction, TransactionForListDto>();
    }
}