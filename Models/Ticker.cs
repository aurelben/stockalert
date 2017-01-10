using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace StockAlert.Models 
{
    public class Ticker {
        public int Id { get; set; }
        public int CurrentPrice { get; set; }

        public string Name { get; set; }

        public int YesterdayPrice { get; set; }

       // public List<User> Subsciber;

    }
}