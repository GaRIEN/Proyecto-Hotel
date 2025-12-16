using Hotel.Core.Domain.Entities;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public GuestsController(HotelDbContext context)
        {
            _context = context;
        }



        // GET: api/guests
        [HttpGet]
        public async Task<IActionResult> GetGuests()
        {
            var guests = await _context.Guests.ToListAsync();
            return Ok(guests);
        }

        // GET: api/guests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuestById(int id)
        {
            var guest = await _context.Guests
                .FirstOrDefaultAsync(g => g.GuestId == id);

            if (guest == null)
                return NotFound();

            return Ok(guest);
        }

        // POST: api/guests
        [HttpPost]
        public async Task<IActionResult> CreateGuest(Guest guest)
        {
            if (guest == null)
                return BadRequest();

            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetGuestById),
                new { id = guest.GuestId },
                guest
            );
        }

        // PUT: api/guests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuest(int id, Guest guest)
        {
            if (id != guest.GuestId)
                return BadRequest("El ID no coincide");

            var existingGuest = await _context.Guests
                .FirstOrDefaultAsync(g => g.GuestId == id);

            if (existingGuest == null)
                return NotFound();

            existingGuest.FirstName = guest.FirstName;
            existingGuest.LastName = guest.LastName;
            existingGuest.DocumentNumber = guest.DocumentNumber;
            existingGuest.Email = guest.Email;
            existingGuest.Phone = guest.Phone;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/guests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var guest = await _context.Guests
                .FirstOrDefaultAsync(g => g.GuestId == id);

            if (guest == null)
                return NotFound();

            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}/reservations")]
        public async Task<IActionResult> GetGuestReservations(int id)
        {
            var guest = await _context.Guests
                .Include(g => g.Reservations)
                .ThenInclude(r => r.Room)
                .FirstOrDefaultAsync(g => g.GuestId == id);

            if (guest == null)
                return NotFound();

            return Ok(guest);
        }


    }
}
