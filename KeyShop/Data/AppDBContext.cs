using KeyShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace KeyShop.Data {
    public class AppDBContext : IdentityDbContext<User> {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) {}
        public DbSet<Game> Games { get; set; }
    }
}
