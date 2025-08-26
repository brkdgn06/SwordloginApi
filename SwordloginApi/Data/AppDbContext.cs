using Microsoft.EntityFrameworkCore;
using SwordloginApi.Models;

namespace SwordloginApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<PrivateReport> PrivateReports { get; set; }
    }
}
