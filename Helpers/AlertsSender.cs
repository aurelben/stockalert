using System;
using System.Collections.Generic;
using StockAlert.Models;
using System.Linq;
namespace StockAlert.Helpers
{
    public class AlertsSender
    {   
        private List<Alert> _alerts;
        private readonly AlertContext _context;
        public AlertsSender (List<Alert> alerts, AlertContext context){
           this._alerts = alerts;
           _context = context;
        }

        public bool SendByMail()
        {   
            foreach (Alert  a in this._alerts){
                Author auth = _context.Authors.FirstOrDefault(x => x.Id == a.AuthorId);
                Console.WriteLine("SEND ");
                Console.WriteLine(a.Title);
                Console.WriteLine("TO ");
                Console.WriteLine(auth.Email.ToString());
                
            }
            return true;
        }
    }
}