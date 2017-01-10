using Microsoft.EntityFrameworkCore;

namespace StockAlert.Models
{
    public class AlertContext : DbContext
    {
        public AlertContext(DbContextOptions<AlertContext> options) : base(options) { 

        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Ticker> Tickers { get; set; }
        
    }
}

