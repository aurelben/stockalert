using Microsoft.EntityFrameworkCore;

namespace StockAlert.Models
{
    public class AlertContext : DbContext
    {
         protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                 modelBuilder.Entity<Ticker>().HasKey(c => c.Name );
               
                 modelBuilder.Entity<Ticker>().Property(f => f.Id).ValueGeneratedOnAdd();

                 modelBuilder.Entity<Alert>().HasKey(c => c.Id);

                 modelBuilder.Entity<Alert>().HasOne(a => a.Ticker).WithMany(t => t.Alerts);
            }    
        public AlertContext(DbContextOptions<AlertContext> options) : base(options) 
        {    

        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Ticker> Tickers { get; set; }
        
    }
}

