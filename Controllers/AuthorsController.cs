using System;
using System.Collections.Generic;
using System.Linq;
using StockAlert.Models;
using Microsoft.AspNetCore.Mvc;

namespace StockAlert.Controllers
{
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly AlertContext _context;
        public AuthorsController(AlertContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IEnumerable<Author> Get()
        {
            return _context.Authors.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Author Get(int id)
        {
            return _context.Authors.FirstOrDefault(x => x.Id == id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Author value)
        {
            _context.Authors.Add(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }
        // PUT api/values/5
        [HttpPut("{Id}")]
        public IActionResult Put([FromBody]Author value, [FromRoute]int id)
        {
            if (value == null )
            {
                return BadRequest();
            }

            Author MyAuthor = _context.Authors.Find(id);
            if (MyAuthor == null)
            {
                return NotFound();
            }
            _context.Entry(MyAuthor).CurrentValues.SetValues(value);
            _context.SaveChanges();
            return StatusCode(201, value);
        }
    }
}
