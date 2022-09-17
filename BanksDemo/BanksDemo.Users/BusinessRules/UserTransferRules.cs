using BanksDemo.User.Constants;
using BanksDemo.User.Repositories.Abstract;
using BanksDemo.User.Services.Abstract;

namespace BanksDemo.User.BusinessRules;

public class UserTransferRules
{
    private readonly IUserRepository _userRepository;
    private readonly IWalletService _walletService;
    public UserTransferRules(IUserRepository userRepository, IWalletService walletService)
    {
        _userRepository = userRepository;
        _walletService = walletService;
    }

    public async Task<Tuple<bool, string>> IsUserFoundAndActiveByUserId(string id)
    {
       var user=await _userRepository.GetByIdAsync(id);
   
       if (user==null)
           return new Tuple<bool, string>(false, BusinessRulesMessages.UserNotFound);
       return !user.IsActive ? new Tuple<bool, string>(false, BusinessRulesMessages.UserNotActive) : new Tuple<bool, string>(true, BusinessRulesMessages.ValidationSuccess);
    }

    public async Task<Tuple<bool, string>> UserWalletCheck(string fromUserId, decimal amount,bool isFrom)
    {
        var walletResponse = await _walletService.GetWalletByUserIdAsync(fromUserId);
        if (walletResponse == null)
            return new Tuple<bool, string>(false, BusinessRulesMessages.WalletNotFound);
        if (walletResponse.IsLock)
            return new Tuple<bool, string>(false, BusinessRulesMessages.WalletIsLock);
        return isFrom && walletResponse.Balance - amount < 0
            ? new Tuple<bool, string>(false, BusinessRulesMessages.AmountInNotEnough)
            : new Tuple<bool, string>(true, BusinessRulesMessages.ValidationSuccess);
    }
     
}