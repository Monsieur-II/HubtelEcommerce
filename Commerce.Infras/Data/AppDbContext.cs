using Commerce.Infras.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Infras.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
