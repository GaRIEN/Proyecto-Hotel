using Hotel.Application.DTOs.Reservations;
using Hotel.Application.Services;
using Hotel.Core.Domain.Entities;
using Hotel.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Hotel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly HotelDbContext _context;
        private readonly ReservationService _reservationService;


        public ReservationsController(HotelDbContext context, ReservationService reservationService)
        {
            _context = context;
            _reservationService = reservationService;
        }

        // GET: api/reservations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(int id)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        // POST: api/reservations
        [HttpPost]
        public async Task<IActionResult> CreateReservation(ReservationCreateDto dto)
        {
            var result = await _reservationService.CreateReservationAsync(dto);

            if (!result.Success)
                return BadRequest(result.Error);

            return Ok("Reserva creada correctamente");
        }

        // PUT: api/reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, Reservation reservation)
        {
            if (id != reservation.ReservationId)
                return BadRequest("El ID no coincide");

            var existingReservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (existingReservation == null)
                return NotFound();

            existingReservation.RoomId = reservation.RoomId;
            existingReservation.GuestId = reservation.GuestId;
            existingReservation.CheckInDate = reservation.CheckInDate;
            existingReservation.CheckOutDate = reservation.CheckOutDate;
            existingReservation.Status = reservation.Status;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null)
                return NotFound();

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetReservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Room)
                .Include(r => r.Guest)
                .ToListAsync();

            return Ok(reservations);
        }

    }
}
