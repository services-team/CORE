using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservations.Database;
using Microsoft.AspNetCore.Identity;
using Reservations.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Reservations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ServiceController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/Service
        [HttpGet]
        public IEnumerable<Service> GetService()
        {
            var context = new ServicesDbContext();

            return context.Service.ToList();
        }

        // GET: api/Service/MyServices
        [HttpGet]
        [Authorize]
        [Route("MyServices")]
        public IEnumerable<Service> MyServices()
        {
            var context = new ServicesDbContext();

            var userId = User.Claims.First(x => x.Type == "UserID").Value;

            return context.Service.Where(x => x.FkUserId == userId).ToList();
        }

        // GET: api/Service/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService([FromRoute] int id)
        {
            var _context = new ServicesDbContext();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = await _context.Service.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT: api/Service/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService([FromRoute] string id, [FromBody] Service service)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.Id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Service
        [HttpPost]
        public async Task<IActionResult> PostService([FromBody] Service service)
        {
            var _context = new ServicesDbContext();

            service.FkUserId = User.Claims.First(x => x.Type == "UserID").Value;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Service.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        // DELETE: api/Service/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService([FromRoute] int id)
        {
            var _context = new ServicesDbContext();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = await _context.Service.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Service.Remove(service);
            await _context.SaveChangesAsync();

            return Ok(service);
        }

        private bool ServiceExists(string id)
        {
            var _context = new ServicesDbContext();
            return _context.Service.Any(e => e.Id == id);
        }
    }
}