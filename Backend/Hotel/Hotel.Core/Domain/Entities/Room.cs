

namespace Hotel.Core.Domain.Entities
{
    public class Room
    {
        public int RoomId { get; set; }      
        public string Number { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
