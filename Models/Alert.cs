using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAlert.Models
{
    public enum AlertType {
        bullish = 0,
        bearish = 1,
        price = 2
    }
    public class Alert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public AlertType Type { get; set; }
        public int Variation { get; set; }
        public Ticker Ticker { get; set; }
        public string TickerName { get; set;}
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
