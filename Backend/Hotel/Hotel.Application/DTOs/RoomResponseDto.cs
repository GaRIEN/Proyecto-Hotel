namespace Hotel.Application.DTOs.Rooms
{
    public class RoomResponseDto
    {
        public int RoomId { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
