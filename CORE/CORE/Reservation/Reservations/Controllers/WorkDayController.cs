using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservations.Database;

namespace Reservations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkDayController : ControllerBase
    {
        // GET: api/WorkDay
        [HttpGet]
        public IEnumerable<Day> GetDay()
        {
            var context = new ServicesDbContext();

            return context.Day.ToList();
        }

        // GET: api/WorkDay/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDay([FromRoute] int id)
        {
            var _context = new ServicesDbContext();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Day = await _context.Day.FindAsync(id);

            if (Day == null)
            {
                return NotFound();
            }

            return Ok(Day);
        }

        // PUT: api/WorkDay/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDay([FromRoute] int id, [FromBody] Day Day)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Day.Id)
            {
                return BadRequest();
            }

            _context.Entry(Day).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayExists(id))
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

        // POST: api/WorkDay
        [HttpPost]
        public async Task<IActionResult> PostDay([FromBody] Day Day)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Day.Add(Day);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDay", new { id = Day.Id }, Day);
        }

        // DELETE: api/WorkDay/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDay([FromRoute] int id)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Day = await _context.Day.FindAsync(id);
            if (Day == null)
            {
                return NotFound();
            }

            _context.Day.Remove(Day);
            await _context.SaveChangesAsync();

            return Ok(Day);
        }

        private bool DayExists(int id)
        {
            var _context = new ServicesDbContext();
            return _context.Day.Any(e => e.Id == id);
        }
    }
}