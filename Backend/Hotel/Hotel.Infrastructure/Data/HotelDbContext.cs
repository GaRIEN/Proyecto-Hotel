
using Microsoft.EntityFrameworkCore;
using Hotel.Core.Domain.Entities;


namespace Hotel.Infrastructure.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options)
            : base(options)
        {
        }
        public DbSet<Room> Rooms { get; set; }

    }
}
