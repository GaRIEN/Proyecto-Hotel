namespace Hotel.Core.Domain.Entities
{
    public class Guest
    {
        public int GuestId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DocumentNumber { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

    }
}
