using MongoDB.Driver;

namespace BanksDemo.User.Contexts;

public interface IUserContext
{
    IMongoCollection<Models.User> Users { get; }
}