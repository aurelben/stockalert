using Microsoft.EntityFrameworkCore;

namespace StockAlert.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int Variation { get; set; }
        public Ticker Ticker { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
