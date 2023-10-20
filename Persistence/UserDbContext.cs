
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DistLab2.Persistence
{
    public class UserDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<AppUserDb> Users { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }


        
    }
}