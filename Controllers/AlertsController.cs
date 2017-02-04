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
    public class AlertsController : Controller
    {
        private readonly AlertContext _context;
        private readonly ILogger _logger;
        public AlertsController(AlertContext context, ILogger<AlertsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /<controller>/
        public IEnumerable<Alert> Get()
        {
            return _context.Alerts.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Alert Get(int id)
        {
            return _context.Alerts.FirstOrDefault(x => x.Id == id);
        }
        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Alert value)
        {

            string tic = value.TickerName;
            string tickerToGet; //= Helpers.GoogleFinanceApiConnector.GetRequest(tic);
            GoogleFinanceObject GFinObj; //= JsonConvert.DeserializeObject<GoogleFinanceObject>(tickerToGet);
            Ticker ticker = _context.Tickers.Find(tic.ToUpper());
            _logger.LogInformation("Ticker value for {tic} from db is: {ticker}", tic, ticker);
            try
            {
                Ticker myTicker = new Ticker();
                if (ticker == null)
                {
                    tickerToGet = Helpers.GoogleFinanceApiConnector.GetRequest(tic);
                    GFinObj = JsonConvert.DeserializeObject<GoogleFinanceObject>(tickerToGet);
                    myTicker.Name = GFinObj.t;
                    myTicker.CurrentPrice = GFinObj.l;
                    _context.Tickers.Add(myTicker);
                    Console.WriteLine("ticketToGet: ");
                    Console.WriteLine(tickerToGet);
                }
                else
                {
                    myTicker = ticker;
                }

                Author aut = _context.Authors.Find(value.AuthorId);                
                if (aut != null)
                {
                    Alert a = new Alert();
                    a.Title = value.Title;
                    a.Type = (AlertType)System.Enum.Parse(typeof(AlertType), value.Type.ToString());
                    a.TickerName = myTicker.Name;
                    a.Variation = value.Variation;
                    a.Ticker = myTicker;
                    a.Author = aut;
                    a.AuthorId = aut.Id;
                    _context.Alerts.Add(a);
                    _context.SaveChanges();
                }
                else
                {
                    _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND,
                        "AlertsController.Post({tic}) Author {aut} NOT FOUND",
                        tic, value.AuthorId);
                    return BadRequest("Authord id " + value.AuthorId + " NOT FOUND");
                }

            }
            catch (Exception e)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND,
                    "AlertsController.Post({tic}) NOT FOUND, Exception: {e}: InnerException: {ie}", tic, e.Message, e.InnerException.Message);
                Console.WriteLine("Exception from TickerCtrl POST route: {1} {2}", e, tic);
                return StatusCode(404, "Can't Add Alert for " + tic);
            }


            return StatusCode(201, "Added Alert for " + tic);
        }
    }
}
