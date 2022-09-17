using BanksDemo.Shared.Events.Abstract;

namespace BanksDemo.Shared.Events.Concrete;

public class FailWalletCreated:IFailWalletCreated
{
    public FailWalletCreated(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; set; }
}