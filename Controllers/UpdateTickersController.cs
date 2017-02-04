using System;
using System.Collections.Generic;
using System.Linq;
using StockAlert.Models;
using Microsoft.AspNetCore.Mvc;

namespace StockAlert.Controllers
{
    [Route("api/[controller]")]
    public class UpdateTickers : Controller
    {
        private readonly AlertContext _context;
        public UpdateTickers(AlertContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IEnumerable<Author> Get()
        {
            return _context.Authors.ToList();
        }

    }    
}
