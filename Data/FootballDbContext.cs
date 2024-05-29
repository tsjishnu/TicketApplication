using Microsoft.EntityFrameworkCore;
using TicketApplication.Models;

namespace TicketApplication.Data
{
    public class FootballDbContext : DbContext
    {
        public FootballDbContext(DbContextOptions options): base(options)
        {
        }
        public DbSet<User>UserTable { get; set; }
        public DbSet<Match> MatchTable { get; set; }
        public DbSet<Order> OrderTable { get; set; }
    }
}
