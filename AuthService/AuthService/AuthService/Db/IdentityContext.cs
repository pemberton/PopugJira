using AuthService.BO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Db
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnConfiguring
            (DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=popugJiraAuth;User ID=sa;Password=SuperPassword123);Max Pool Size=1000;Pooling=true;Connection Timeout=5;");
    }
}