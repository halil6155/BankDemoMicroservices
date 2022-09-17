using BanksDemo.User.DTOs;

namespace BanksDemo.User.Repositories.Abstract;

public interface IUserRepository
{
    Task<IEnumerable<UserForListDto>> GetAllAsync();
    Task<bool> DeleteAsync(string userId);
    Task<bool> CreateAsync(UserForRegisterDto userForRegisterDto);
    Task<UserForListDto?> GetByIdAsync(string id);
}