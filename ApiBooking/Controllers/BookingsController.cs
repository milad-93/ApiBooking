using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBooking.Models;
using Microsoft.AspNetCore.Cors;


namespace ApiBooking.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("TheProductsPolicy")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingContext _context;
        

        public BookingsController(BookingContext context)
        {
            _context = context;
        }

        // GET: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetPerson()
        {
            //https://entityframework.net/improve-ef-include-performance
            //_context.Person.Include(x=>x.BookableHours).Where(x=>x.Id==65); 
            //  https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager

            var bookingList = await _context.Person.Include(x => x.BookableHours).ToListAsync();

            return bookingList;

           
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetPerson(int id)
        {
            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Booking person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

    
        [HttpPost]
        public async Task<ActionResult<Booking>> PostPerson(Booking person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booking>> DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
