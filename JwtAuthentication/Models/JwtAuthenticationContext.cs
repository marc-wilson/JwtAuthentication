using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthentication.Models
{
    public class JwtAuthenticationContext : IdentityDbContext<ApplicationUser>
    {
        public JwtAuthenticationContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}