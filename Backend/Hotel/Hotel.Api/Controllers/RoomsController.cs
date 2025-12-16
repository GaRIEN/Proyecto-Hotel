using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel.Core.Domain.Entities;

namespace Hotel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomsController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/rooms
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return Ok(rooms);
        }

        // GET: api/rooms/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
                return NotFound();

            return Ok(room);
        }

        // POST: api/rooms
        [HttpPost]
        public async Task<IActionResult> CreateRoom(Room room)
        {
            if (room == null)
                return BadRequest();

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRoomById),
                new { id = room.RoomId },
                room
            );
        }
        // PUT: api/rooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, Room room)
        {
            if (id != room.RoomId)
                return BadRequest("El ID no coincide");

            var existingRoom = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (existingRoom == null)
                return NotFound();

            existingRoom.Number = room.Number;
            existingRoom.Type = room.Type;
            existingRoom.Price = room.Price;
            existingRoom.Status = room.Status;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
                return NotFound();

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }



    }
}
