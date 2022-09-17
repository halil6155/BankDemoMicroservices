using MongoDB.Driver;

namespace BanksDemo.User.Contexts;

public class SeedData
{
    public static void InsertDefaultData(IMongoCollection<Models.User> userCollection)
    {
        var exist = userCollection.Find(p => true).Any();
        if (exist) return;
        userCollection.InsertManyAsync(GetDefaultUsers());
    }
    private static IEnumerable<Models.User> GetDefaultUsers()
    {
        return new List<Models.User>
            {
                new()
                {
                   CreatedOn = DateTime.Now,
                   FirstName = "Default",
                   IsActive = true,
                   LastName = "User",
                },
                new()
                {
                    CreatedOn = DateTime.Now,
                    FirstName = "Default-2",
                    IsActive = true,
                    LastName = "User-2"
                },
            };
    }
}