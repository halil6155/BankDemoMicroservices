

using BanksDemo.Shared.Interfaces;

namespace BanksDemo.User.DTOs;

public class UserForListDto:IDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName

    { get; set; }

    public DateTime CreatedOn { get; set; }
    public bool IsActive { get; set; }
}