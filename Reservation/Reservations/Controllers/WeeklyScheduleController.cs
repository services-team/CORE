using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservations.Database;
using Reservations.ViewModels;

namespace Reservations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeklyScheduleController : ControllerBase
    {
        // GET: api/WeeklySchedule
        [HttpGet]
        public IEnumerable<WeeklyScheduleViewModel> GetWeeklySchedule()
        {
            var context = new ServicesDbContext();

            //var a = context.WeeklySchedule.Include("Day.WeekDay").ToList();
            var a = context.WeeklySchedule.Include("Day.WeekDay").Include("FkUser").Include("Day.WorkTime").ToList();
            var b = context.Day.Include("WorkTime").ToList();
            //var b = context.WeeklySchedule;//.Include("Day");
            //a.FirstOrDefault().Day.Add(new Day { Id = 1, WeekDayId = 2, ScheduleId = 1, WeekDay = new WeekDays { Name = "monday" }  });
            //a.FirstOrDefault().Day.Add(new Day { Id = 1, WeekDayId = 2, ScheduleId = 1, WeekDay = new WeekDays { Name = "tuesday" } });
            //b.Last().Day.Add(new Day { Id = 1, WeekDayId = 2, ScheduleId = 1, WeekDay = new WeekDays { Name = "monday" } });
            //var c = b.ToList();
            List<WeeklyScheduleViewModel> asd = WeeklyScheduleViewModel.Convert(a);

            return asd;
        }

        // GET: api/WeeklySchedule/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeeklySchedule([FromRoute] int id)
        {
            var _context = new ServicesDbContext();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var WeeklySchedule = await _context.WeeklySchedule.FindAsync(id);

            if (WeeklySchedule == null)
            {
                return NotFound();
            }

            return Ok(WeeklySchedule);
        }

        // PUT: api/WeeklySchedule/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeeklySchedule([FromRoute] int id, [FromBody] WeeklySchedule WeeklySchedule)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != WeeklySchedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(WeeklySchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeeklyScheduleExists(id))
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

        // POST: api/WeeklySchedule
        [HttpPost]
        public async Task<IActionResult> PostWeeklySchedule([FromBody] WeeklySchedule WeeklySchedule)
        {
            var _context = new ServicesDbContext();

            WeeklySchedule.FkUserId = "645b38af-d971-4cdb-949f-66e3b86dccbc";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WeeklySchedule.Add(WeeklySchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeeklySchedule", new { id = WeeklySchedule.Id }, WeeklySchedule);
        }

        // DELETE: api/WeeklySchedule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeeklySchedule([FromRoute] int id)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var WeeklySchedule = await _context.WeeklySchedule.FindAsync(id);
            if (WeeklySchedule == null)
            {
                return NotFound();
            }

            _context.WeeklySchedule.Remove(WeeklySchedule);
            await _context.SaveChangesAsync();

            return Ok(WeeklySchedule);
        }

        private bool WeeklyScheduleExists(int id)
        {
            var _context = new ServicesDbContext();
            return _context.WeeklySchedule.Any(e => e.Id == id);
        }
    }
}