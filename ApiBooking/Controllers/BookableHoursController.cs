using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiBooking.Models;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using LoggerService;
using Contracts;

namespace ApiBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("TheProductsPolicy")]
    public class BookableHoursController : ControllerBase
    {
        private readonly BookingContext _context;
        private readonly ILoggerManager _logger;

        public BookableHoursController(BookingContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/BookableHours
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookableHours>>> GetBookableHours()
        {
            _logger.LogInfo("Fetching all bookable hours");
            var ListOfAvaiableBookingTimes = await _context.BookableHours.OrderBy(x => x).ToListAsync();
            return ListOfAvaiableBookingTimes;
        
            
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<BookableHours>> GetBookableHours(int id)
        {
            _logger.LogInfo("Fetching listed bookable hour by ID");
            var bookableHours = await _context.BookableHours.FindAsync(id);

            if (bookableHours == null)
            {
                _logger.LogError("Requested Boobkeable hour does not exist");
                return StatusCode(500, "Internal Server error");
            }

            return bookableHours; 
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookableHours(int id, BookableHours bookableHours)
        {
            if (id != bookableHours.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookableHours).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookableHoursExists(id))
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
        public async Task<ActionResult<BookableHours>> PostBookableHours(BookableHours bookableHours)
        {
            _context.BookableHours.Add(bookableHours);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookableHours", new { id = bookableHours.Id }, bookableHours);
        }

        // DELETE: api/BookableHours/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookableHours>> DeleteBookableHours(int id)
        {
            var bookableHours = await _context.BookableHours.FindAsync(id);
            if (bookableHours == null)
            {
                return NotFound();
            }

            _context.BookableHours.Remove(bookableHours);
            await _context.SaveChangesAsync();

            return bookableHours;
        }

        private bool BookableHoursExists(int id)
        {
            return _context.BookableHours.Any(e => e.Id == id);
        }
    }
}
