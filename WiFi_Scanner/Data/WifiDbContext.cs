using Microsoft.EntityFrameworkCore;
using WiFi_Scanner.Models;

namespace WiFi_Scanner.Data
{
    public class WifiDbContext : DbContext
    {
        public DbSet<WifiNetwork> WifiNetworks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=your_database_name;Username=your_username;Password=your_password");
        }
    }
}