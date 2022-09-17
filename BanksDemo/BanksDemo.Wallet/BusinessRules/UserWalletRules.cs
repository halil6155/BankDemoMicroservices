using BanksDemo.Wallet.Constants;
using BanksDemo.Wallet.DTOs;

namespace BanksDemo.Wallet.BusinessRules;

public class UserWalletRules
{
    public Tuple<bool, string> WalletCheckByWalletListDtoForTransfer(WalletForListDto? walletForListDto)
    {
        if (walletForListDto == null)
            return new Tuple<bool, string>(false, BusinessRulesMessages.WalletNotFound);
        return walletForListDto.IsLock ? new Tuple<bool, string>(false, BusinessRulesMessages.WalletIsLock) :
            new Tuple<bool, string>(true, BusinessRulesMessages.ValidationSuccess);
    }

    public Tuple<bool, string> WalletAmountCheck(decimal amount, decimal requestAmount)
    =>
          amount < requestAmount ? new Tuple<bool, string>(false, BusinessRulesMessages.AmountInNotEnoughForTransfer) : new Tuple<bool, string>(true, BusinessRulesMessages.ValidationSuccess);
}