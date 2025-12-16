namespace Hotel.Application.DTOs.Reservations
{
    public class ReservationCreateDto
    {
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
