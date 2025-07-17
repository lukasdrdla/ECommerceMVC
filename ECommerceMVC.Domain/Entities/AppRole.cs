using Microsoft.AspNetCore.Identity;

namespace ECommerceMVC.Domain.Entities;

public class AppRole : IdentityRole
{
    public string? Description { get; set; }

}