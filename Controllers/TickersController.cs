using System;
using System.Collections.Generic;
using System.Linq;
using StockAlert.Models;
using StockAlert.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace StockAlert.Controllers
{
    [Route("api/[controller]")]
    public class TickersController : Controller
    {
        private readonly AlertContext _context;
        public TickersController(AlertContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Get()
        {
            List<Ticker> Tickers = _context.Tickers.ToList();
            List<Alert> AlertsBull;
            List<Alert> AlertsBear;
            List<Alert> AlertsPrice;
            Console.WriteLine("before foreach");
            Tickers.ForEach(Console.WriteLine);
            foreach( Ticker t in Tickers )
            {
                string ticker = t.Name;
                string tickerToGet = Helpers.GoogleFinanceApiConnector.GetRequest(ticker);
                GoogleFinanceObject GFinObj = JsonConvert.DeserializeObject<GoogleFinanceObject>(tickerToGet);

                t.Change = GFinObj.c;
                t.CurrentPrice = GFinObj.l_cur;
                List<Author> Subscribers = t.Subscibers;

                double change;
                GFinObj.c_fix = GFinObj.c_fix.Replace('.', ',');
                double.TryParse(GFinObj.c_fix, out change);
                Console.WriteLine("Change is: \n");
                Console.WriteLine(change);
                if (change < 0)
                {
                    // Get all bearish Alerts
                    AlertsBear = _context.Alerts.Where(a => (a.Type == AlertType.bearish) && (a.TickerName == t.Name) ).ToList();
                    Console.WriteLine("Get all bearish Alerts");
                    AlertsBear.ForEach(Console.WriteLine);
                    var  sendBear = new Helpers.AlertsSender(AlertsBear, _context);
                    sendBear.SendByMail();
                }
                else if (change > 0)
                {
                    // Get all bullsih Alerts
                    AlertsBull = _context.Alerts.Where(a => (a.Type == AlertType.bullish) && (a.TickerName == t.Name) ).ToList();
                    Console.WriteLine("Get all bullsih Alerts");
                    AlertsBull.ForEach(Console.WriteLine);
                    var  sendBull = new Helpers.AlertsSender(AlertsBull, _context);
                    sendBull.SendByMail();
                }
                // Get all price Alers
                AlertsPrice = _context.Alerts.Where(a => (a.Type == AlertType.price) && (a.TickerName == t.Name) ).ToList();
                Console.WriteLine("Get all price Alers");
                AlertsPrice.ForEach(Console.WriteLine);
                var  sendPrice = new Helpers.AlertsSender(AlertsPrice, _context);
                sendPrice.SendByMail();
            }

            return StatusCode(200, "Ok");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Ticker Get(int id)
        {
            return _context.Tickers.FirstOrDefault(x => x.Id == id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromRoute]string ticker)
        {
            // _context.Tickers.Add();
            // _context.SaveChanges();
            return StatusCode(201, "ok");
        }
        // PUT api/values/5
        [HttpPut("{Id}")]
        public IActionResult Put([FromBody]Ticker value, [FromRoute]int id)
        {
            if (value == null )
            {
                return BadRequest();
            }

            Author MyTicker = _context.Authors.Find(id);
            if (MyTicker == null)
            {
                return NotFound();
            }
            _context.Entry(MyTicker).CurrentValues.SetValues(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }
    }
}
