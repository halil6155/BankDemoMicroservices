using BanksDemo.Shared.Events.Abstract;

namespace BanksDemo.Shared.Events.Concrete;

public class UserCreatedEvent:IUserCreatedEvent
{
    public UserCreatedEvent(string firstName, string lastName, string userId)
    {
        FirstName = firstName;
        LastName = lastName;
        UserId = userId;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserId { get; set; }
}