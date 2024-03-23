using Microsoft.AspNetCore.Identity;

namespace Commerce.Infras.Models;

public class AppUser : IdentityUser
{
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime LastLogin { get; set; }
}
