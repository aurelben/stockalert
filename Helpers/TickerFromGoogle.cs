using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using StockAlert.Models;
using StockAlert.Helpers;
using System;


namespace StockAlert.Helpers
{
    public class GoogleFinanceObject
        {
            public string id;
            public string t;
            public string e;
            public string l;
            public string l_fix;
            public string l_cur;
            public string s;
            public string ltt;
            public string lt;
            public string lt_dts;
            public string c;
            public string c_fix;
            public string cp;
            public string cp_fix;
            public string ccol;
            public string pcls_fix;
            public string el;
            public string el_fix;
            public string el_cur;
            public string elt;
            public string ec;
            public string ec_fix;
            public string ecp;
            public string ecp_fix;
            public string eccol;
            public string div;
            public string yld;
        }
    public class TickerFromGoogle
    {
        public bool GetTicker( Alert value,  AlertContext _context, ILogger<TickerFromGoogle> _logger)
        {
            string tic = value.TickerName;
            string tickerToGet; //= Helpers.GoogleFinanceApiConnector.GetRequest(tic);
            GoogleFinanceObject GFinObj; //= JsonConvert.DeserializeObject<GoogleFinanceObject>(tickerToGet);
            Ticker ticker = _context.Tickers.Find(tic.ToUpper());
            _logger.LogInformation("Ticker value for {tic} from db is: {ticker}", tic, ticker);
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
                //return BadRequest("Authord id " + value.AuthorId + " NOT FOUND");
            }
            return true;
        }
    }
}