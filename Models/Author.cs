using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAlert.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token {get; set;}
        public List<Alert> Alerts { get; set; } = new List<Alert>();
    }
}
