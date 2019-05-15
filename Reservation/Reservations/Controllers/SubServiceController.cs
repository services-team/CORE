using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservations.Database;
using Reservations.Models;

namespace Reservations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubServiceController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SubServiceController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        // GET: api/SubService
        [HttpGet]
        public IEnumerable<SubService> GetSubService()
        {
            var context = new ServicesDbContext();

            return context.SubService.ToList();
        }

        // GET: api/SubService/5
        [HttpGet("{id}")]
        public SubService GetSubService([FromRoute] int id)
        {
            var context = new ServicesDbContext();

            return context.SubService.Where(x => x.Id == id).FirstOrDefault();
        }

        // PUT: api/SubService/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubService([FromRoute] int id, [FromBody] SubService subservice)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subservice.Id)
            {
                return BadRequest();
            }

            _context.Entry(subservice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubServiceExists(id))
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

        // POST: api/SubService
        [HttpPost]
        public async Task<IActionResult> PostSubService([FromBody] SubService subservice)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SubService.Add(subservice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubService", new { id = subservice.Id }, subservice);
        }

        // DELETE: api/SubService/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubService([FromRoute] int id)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subservice = await _context.SubService.FindAsync(id);
            if (subservice == null)
            {
                return NotFound();
            }

            _context.SubService.Remove(subservice);
            await _context.SaveChangesAsync();

            return Ok(subservice);
        }

        private bool SubServiceExists(int id)
        {
            return (new ServicesDbContext().SubService.Any(x => x.Id == id));
        }
    }
}