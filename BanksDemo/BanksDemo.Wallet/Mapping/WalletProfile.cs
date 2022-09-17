using AutoMapper;
using BanksDemo.Shared.Events.Abstract;
using BanksDemo.Wallet.DTOs;

namespace BanksDemo.Wallet.Mapping;

public class WalletProfile:Profile
{
    public WalletProfile()
    {
        CreateMap<IUserCreatedEvent, WalletForCreateDto>();
        CreateMap<WalletForCreateDto, Models.Wallet>();
        CreateMap<Models.Wallet, WalletForListDto>();
    }
}