using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Just_project.Admin.Models
{
    //public class AppDbContext : DbContext
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        //public DbSet<AppUser> Users { get; set; } 
        //public DbSet<SignInModel> SignInModels { get; set; }
    }

    
}