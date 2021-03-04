using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderAppWebApi.Data;
using OrderAppWebApi.Models;

namespace OrderAppWebApi.Controllers
{
    [Route("api/[controller]")] // a attribute that Identify that the user wants a method out of this controller not another, there is no reason to change this.
    [ApiController] // this formats Json for us, don't change this ever
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context; // our Dbcontext instance

        public CustomersController(AppDbContext context) 
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet] // this is a HTTP method, it uses GET, we use this anytime we are reading data
        // method will get all customers, reading them all and returning it as a list as async
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers() // ActionResult is a way to return error messages back to the client.
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")] // this is going to give a HTTP GET, it has to add a variable
        // this is reading the customer by the Id, if not found
        public async Task<ActionResult<Customers>> GetCustomers(int id)
        {
            var customers = await _context.Customers.FindAsync(id);

            if (customers == null)
            {
                return NotFound();
            }

            return customers;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")] // this HTTP method is using put, this will change/update in customer.
        public async Task<IActionResult> PutCustomers(int id, Customers customers)
        {
            if (id != customers.Id)
            {
                return BadRequest();
            }

            _context.Entry(customers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost] // this is an HTTP Post, this will add/create things within Customers
        public async Task<ActionResult<Customers>> PostCustomers(Customers customers)
        {
            _context.Customers.Add(customers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomers", new { id = customers.Id }, customers);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")] // this is an HTTP Delete, this will delete/remove columns within Customers.
        public async Task<ActionResult<Customers>> DeleteCustomers(int id)
        {
            var customers = await _context.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();

            return customers;
        }

        private bool CustomersExists(int id) // this is private meaning it can't be called from outside this class.
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
