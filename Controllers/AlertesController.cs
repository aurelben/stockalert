using System;
using System.Collections.Generic;
using System.Linq;
using StockAlert.Models;
using Microsoft.AspNetCore.Mvc;

namespace StockAlert.Controllers
{
    [Route("api/[controller]")]
    public class AlertsController : Controller
    {
        private readonly AlertContext _context;
        public AlertsController(AlertContext context)
        {
            _context = context;
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
            return _context.Alerts.FirstOrDefault(x=>x.Id == id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Alert value)
        {
            _context.Alerts.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }
    }
}
