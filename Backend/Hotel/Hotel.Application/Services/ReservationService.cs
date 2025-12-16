//servirqa para hacer validaciones
using Hotel.Application.DTOs.Reservations;
using Hotel.Core.Domain.Entities;
using Hotel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Application.Services
{
    public class ReservationService
    {
        private readonly HotelDbContext _context;

        public ReservationService(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string Error)> CreateReservationAsync(ReservationCreateDto dto)
        {
            if (dto.CheckOutDate <= dto.CheckInDate)
                return (false, "Fechas inválidas");

            var room = await _context.Rooms.FindAsync(dto.RoomId);
            if (room == null)
                return (false, "Habitación no existe");

            var guest = await _context.Guests.FindAsync(dto.GuestId);
            if (guest == null)
                return (false, "Huésped no existe");

            var overlap = await _context.Reservations.AnyAsync(r =>
                r.RoomId == dto.RoomId &&
                r.Status == "Active" &&
                dto.CheckInDate < r.CheckOutDate &&
                dto.CheckOutDate > r.CheckInDate
            );

            if (overlap)
                return (false, "Habitación ocupada");

            var reservation = new Reservation
            {
                RoomId = dto.RoomId,
                GuestId = dto.GuestId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                Status = "Active"
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return (true, null);
        }
    }
}
