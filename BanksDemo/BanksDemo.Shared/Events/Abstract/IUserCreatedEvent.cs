namespace BanksDemo.Shared.Events.Abstract;

public interface IUserCreatedEvent
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserId { get; set; }
}