namespace Hotel.Application.DTOs.Reservations
{
    public class ReservationResponseDto
    {
        public int ReservationId { get; set; }
        public string RoomNumber { get; set; }
        public string GuestName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
    }
}
