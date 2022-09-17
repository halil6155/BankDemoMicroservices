using BanksDemo.User.Settings;
using MongoDB.Driver;

namespace BanksDemo.User.Contexts;

public class UserContext:IUserContext
{
    public UserContext(IDatabaseConfigurationSettings databaseConfigurationSettings)
    {
        var dbConfigurationSettings = databaseConfigurationSettings;
        var client = new MongoClient(dbConfigurationSettings.ConnectionString);
        var database = client.GetDatabase(dbConfigurationSettings.DatabaseName);
        Users = database.GetCollection<Models.User>(dbConfigurationSettings.CollectionName);
        //SeedData.InsertDefaultData(Users);
    }

    public IMongoCollection<Models.User> Users { get; }
}