
namespace Hotel.Core.Domain.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int RoomId { get; set; }
        public int GuestId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public string Status { get; set; }
        // 🔗 Relaciones
        public Room Room { get; set; }
        public Guest Guest { get; set;}
    }
}
