using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BanksDemo.Shared.Interfaces;

namespace BanksDemo.Wallet.Models;

public class Wallet:IEntity
{
    [Key]
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Column(TypeName = ("decimal(18,2)"))]
    public decimal Balance { get; set; }
    public string UserId { get; set; }
    public bool IsLock { get; set; }

}