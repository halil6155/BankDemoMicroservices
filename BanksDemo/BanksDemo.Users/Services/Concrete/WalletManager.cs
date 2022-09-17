using BanksDemo.User.Models;
using BanksDemo.User.Services.Abstract;

namespace BanksDemo.User.Services.Concrete;

public class WalletManager : IWalletService
{
    private readonly HttpClient _httpClient;

    public WalletManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WalletListDto?> GetWalletByUserIdAsync(string userId)
    {
        var result = await _httpClient.GetAsync($"getByUserId/{userId}");
        if (!result.IsSuccessStatusCode)
            return null;
        return await result.Content.ReadFromJsonAsync<WalletListDto>();
    }
}