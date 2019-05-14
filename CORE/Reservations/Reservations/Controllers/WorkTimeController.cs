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
    public class WorkTimeController : ControllerBase
    {
        // GET: api/WorkTime
        [HttpGet]
        public IEnumerable<WorkTime> GetWorkTime()
        {
            var context = new ServicesDbContext();

            return context.WorkTime.ToList();
        }

        // GET: api/WorkTime/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkTime([FromRoute] int id)
        {
            var _context = new ServicesDbContext();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var WorkTime = await _context.WorkTime.FindAsync(id);

            if (WorkTime == null)
            {
                return NotFound();
            }

            return Ok(WorkTime);
        }

        // PUT: api/WorkTime/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkTime([FromRoute] int id, [FromBody] WorkTime WorkTime)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != WorkTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(WorkTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkTimeExists(id))
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

        // POST: api/WorkTime
        [HttpPost]
        public async Task<IActionResult> PostWorkTime([FromBody] WorkTime WorkTime)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WorkTime.Add(WorkTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkTime", new { id = WorkTime.Id }, WorkTime);
        }

        // DELETE: api/WorkTime/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkTime([FromRoute] int id)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var WorkTime = await _context.WorkTime.FindAsync(id);
            if (WorkTime == null)
            {
                return NotFound();
            }

            _context.WorkTime.Remove(WorkTime);
            await _context.SaveChangesAsync();

            return Ok(WorkTime);
        }

        private bool WorkTimeExists(int id)
        {
            var _context = new ServicesDbContext();
            return _context.WorkTime.Any(e => e.Id == id);
        }
    }
}