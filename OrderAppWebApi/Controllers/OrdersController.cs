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
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context) {
            _context = context;
        }

        //PUT: api/orders/Final/5
        [HttpPut("Final/{id}")]
        public async Task<IActionResult> SetOrderStatusToFinal(int id) {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) {
                return NotFound();
            }
            order.Status = "FINAL";
            return await PutOrder(order.Id, order);
        }

        //PUT: api/orders/Proposed/5
        [HttpPut ("Proposed/{id}")]
        public async Task<IActionResult> SetOrderStatusToProposed(int id) {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) {
                return NotFound();
            }
            order.Status = (order.Total <= 100) ? "FINAL" : "PROPOSED";

            //if (order.Total > 100.00m) {
            //     order.Status = "FINAL";
            //} else { 
            //order.Status = "PROPOSED";
            //}
            
            return await PutOrder(order.Id, order);
        }

        // PUT: api/orders/Edit/5
        [HttpPut ("edit/{id}")] // here we are setting the Status within orders to be set to edit, this will change the Status to be set to Edit rather than new when this method of put is used is Postman
        public async Task<IActionResult> SetOrderStatusToEdit(int id) {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) {
                return NotFound();
            }
            order.Status = "EDIT";
             return await PutOrder(order.Id, order); // here we are using and returning the method already created by the controller that updates.
        }

        // GET: api/orders/Proposed
        [HttpGet("proposed")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersInProposedStatus() {
            return await _context.Orders
                                        .Include(c => c.customer)
                                        .Where(o => o.Status == "PROPOSED")
                                        .ToListAsync(); // this is qurery LINQ 
                          
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders
                                        .Include(c => c.customer)
                                        .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                                            .Include(s => s.Salesperson)
                                            .Include(c => c.customer)
                                            .Include(l => l.OrderLines)
                                            .ThenInclude(i => i.Item) // ThenInclude implies to the include right above it. // this has to become after the Orderlines, if we added it after the customer, it's going to think we are trying to add item to customer.
                                            .SingleOrDefaultAsync(o => o.Id == id); // used to be  .FindAsync(id); but was not working // have becca explain this

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
