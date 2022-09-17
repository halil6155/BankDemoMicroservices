using BanksDemo.Shared.Interfaces;

namespace BanksDemo.User.DTOs;

public class TransferRequestDto:IDto
{
    public string CurrentUserId { get; set; }
    public string ToUserId { get; set; }
    public decimal Amount { get; set; }
}