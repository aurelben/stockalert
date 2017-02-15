using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAlert.Models 
{
    public class Ticker {
        public int Id { get; set; }
        public string CurrentPrice { get; set; }
        public string Name { get; set; }
        public string Change { get; set; }
        public List<Author> Subscibers {get; set;} = new List<Author>();
        public List<Alert> Alerts {get; set;} = new List<Alert>();
    }
}