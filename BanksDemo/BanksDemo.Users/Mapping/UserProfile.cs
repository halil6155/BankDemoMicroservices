using AutoMapper;
using BanksDemo.User.DTOs;

namespace BanksDemo.User.Mapping;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<Models.User, UserForListDto>();
        CreateMap<UserForRegisterDto, Models.User>();
    }
}