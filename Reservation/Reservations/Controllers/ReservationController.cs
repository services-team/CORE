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
    public class ReservationController : ControllerBase
    {

        public ReservationController()
        {
        }

        // GET: api/Reservations
        [HttpGet]
        public IEnumerable<Reservation> GetReservation()
        {
            var test = IsValidReservation(1);
            return new ServicesDbContext().Reservation.ToList();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservation = await new ServicesDbContext().Reservation.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // PUT: api/Reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation([FromRoute] int id, [FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.Id)
            {
                return BadRequest();
            }

            var context = new ServicesDbContext();

            context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        [HttpPost]
        public async Task<IActionResult> PostReservation([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var context = new ServicesDbContext();

            context.Reservation.Add(reservation);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var context = new ServicesDbContext();

            var reservation = await context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            context.Reservation.Remove(reservation);
            await context.SaveChangesAsync();

            return Ok(reservation);
        }

        private bool ReservationExists(int id)
        {
            return new ServicesDbContext().Reservation.Any(e => e.Id == id);
        }

        private bool IsValidReservation(int id)
        {
            var context = new ServicesDbContext();

            var reservation = context.Reservation.Where(x => x.Id == id).Include("SubService.Service.FkUser.WeeklySchedule.Day.WorkTime").ToList().First();

            short kelintadienis = Convert.ToInt16(reservation.StartDate.DayOfWeek);

            var workTimes = new List<WorkTime>();

            try
            {
                workTimes = reservation.SubService.Service.FkUser.WeeklySchedule.First().Day.ElementAt(kelintadienis - 1).WorkTime.ToList();
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            //var serviceProvider = context.Reservation.Where(x => x.Id == id).Include("SubService.Service.FkUser").ToList().First().SubService.Service.FkUser;

            //var serviceClient = context.Reservation.Where(x => x.Id == id).Include("FkUser").ToList().First().FkUser;

            //DATOS NETIKRINU, TIK LAIKA 

            int start = reservation.StartDate.TimeOfDay.Minutes + (reservation.StartDate.TimeOfDay.Hours * 60);

            int end = start + reservation.SubService.Duration;

            foreach(var timespan in workTimes)
            {
                if (start >= timespan.MinutesFrom && end <= timespan.MinutesTo)
                    return true;
            }

            return false;
        }
    }
}