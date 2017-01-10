using Microsoft.EntityFrameworkCore;

namespace StockAlert.Models
{
    public class ArticleContext : DbContext
    {
        public ArticleContext(DbContextOptions<ArticleContext> options) : base(options) { 

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Ticker> Tickers { get; set; }
        
    }
}

